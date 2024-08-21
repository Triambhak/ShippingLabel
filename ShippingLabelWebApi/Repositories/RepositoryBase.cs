
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using shippinglabelwebapi.AppDbcontext;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace shippinglabelwebapi.repositories
//{
//    public class repositorybase<tentity> : irepository<tentity> where tentity : class
//    {
//        #region properties
//        protected string entityname => typeof(tentity).name;
//        protected appdbcontext dbcontext { get; set; }
//        #endregion

//        public repositorybase()
//        {
//            var optionsbuilder = new dbcontextoptionsbuilder<appdbcontext>().usequerytrackingbehavior(querytrackingbehavior.notracking);

//            var constr = connectionstring.connection;
//            optionsbuilder.useloggerfactory(connectionstring.dblogger);
//            optionsbuilder.usesqlserver(constr).enablesensitivedatalogging();
//            dbcontext = new appdbcontext(optionsbuilder.options);
//        }
//        public repositorybase(appdbcontext appdbcontext)
//        {
//            dbcontext = appdbcontext;
//        }

//        private int getprimarykeyvalue(tentity entity)
//        {
//            var properties = typeof(tentity).getproperties();
//            int keyvalue = 0;

//            var key = properties.where(p => p.isdefined(typeof(keyattribute), false)).first();
//            if (key == null)
//            {
//                return keyvalue;
//            }

//            keyvalue = convert.toint32(key?.getvalue(entity, null));
//            return keyvalue;

//        }
//        private void dblogger(tentity oldentity, tentity newentity, dboperation dboperation)
//        {
//            var oldjsondata = string.empty;
//            var newjsondata = string.empty;
//            if (newentity != null)
//            {
//                int keyvalue = getprimarykeyvalue(newentity);
//            }
//            switch (dboperation)
//            {
//                case dboperation.create:
//                    newjsondata = jsonconvert.serializeobject(newentity);
//                    break;
//                case dboperation.update:
//                    oldjsondata = jsonconvert.serializeobject(oldentity);
//                    newjsondata = jsonconvert.serializeobject(newentity);
//                    break;
//                case dboperation.delete:
//                    oldjsondata = jsonconvert.serializeobject(oldentity);
//                    break;
//                default:
//                    break;
//            }

//            var newtransaction = new databasetransactions()
//            {
//                tablename = entityname,
//                title = dboperation.tostring(),
//                description = $"record manipulated in {entityname}",
//                newdatajsonformat = newjsondata,
//                olddatajsonformat = oldjsondata,
//                createdbyid = dbcontext.userid,
//                createddatetime = datetime.now
//            };

//            dbcontext.set<databasetransactions>().add(newtransaction);
//            savechanges();
//        }
//        private void dblogger(list<tentity> oldentities,
//            list<tentity> newentities, dboperation dboperation)
//        {
//            var oldjsondata = string.empty;
//            var newjsondata = string.empty;

//            switch (dboperation)
//            {
//                case dboperation.create:
//                    newjsondata = jsonconvert.serializeobject(newentities);
//                    break;
//                case dboperation.update:
//                    oldjsondata = jsonconvert.serializeobject(oldentities);
//                    newjsondata = jsonconvert.serializeobject(newentities);
//                    break;
//                case dboperation.delete:
//                    oldjsondata = jsonconvert.serializeobject(oldentities);
//                    break;
//                default:
//                    break;
//            }

//            var newtransaction = new databasetransactions()
//            {
//                tablename = entityname,
//                title = dboperation.tostring(),
//                description = $"record manipulated in {entityname}",
//                newdatajsonformat = newjsondata,
//                olddatajsonformat = oldjsondata,
//                createdbyid = dbcontext.userid,
//                createddatetime = datetime.now
//            };

//            dbcontext.set<databasetransactions>().add(newtransaction);
//            savechanges();
//        }

//        public void savechanges()
//        {
//            dbcontext.savechanges();
//        }

//        #region syncmethods
//        public void add(tentity entity)
//        {
//            dbcontext.changetracker.querytrackingbehavior = querytrackingbehavior.notracking;
//            dbcontext.set<tentity>().add(entity);
//            savechanges();
//            dblogger(null, entity, dboperation.create);
//        }

//        public list<tentity> add(list<tentity> listofentitites)
//        {
//            dbcontext.changetracker.querytrackingbehavior = querytrackingbehavior.notracking;
//            dbcontext.set<tentity>().addrange(listofentitites);
//            savechanges();
//            dblogger(null, listofentitites, dboperation.create);
//            return listofentitites;
//        }

//        public tentity addreturn(tentity entity)
//        {
//            dbcontext.changetracker.querytrackingbehavior = querytrackingbehavior.notracking;
//            dbcontext.set<tentity>().add(entity);
//            savechanges();
//            dblogger(null, entity, dboperation.create);
//            return entity;
//        }

//        public void update(tentity entity)
//        {
//            int keyvalue = getprimarykeyvalue(entity);
//            var oldentity = getbyid(keyvalue);
//            dbcontext.changetracker.querytrackingbehavior = querytrackingbehavior.notracking;
//            dbcontext.set<tentity>().update(entity);
//            savechanges();
//            dblogger(oldentity, entity, dboperation.update);
//        }

//        public tentity updatereturn(tentity entity)
//        {
//            int keyvalue = getprimarykeyvalue(entity);
//            var oldentity = getbyid(keyvalue);

//            dbcontext.set<tentity>().update(entity);
//            savechanges();
//            dblogger(oldentity, entity, dboperation.update);
//            return entity;
//        }

//        public void update(list<tentity> listofentitites)
//        {
//            var olditems = listofentitites;
//            dbcontext.set<tentity>().updaterange(listofentitites);
//            savechanges();
//            dblogger(olditems, listofentitites, dboperation.update);
//        }

//        public list<tentity> updatereturn(list<tentity> listofentitites)
//        {
//            var olditems = listofentitites;
//            dbcontext.set<tentity>().updaterange(listofentitites);
//            savechanges();
//            dblogger(olditems, listofentitites, dboperation.update);
//            return listofentitites;
//        }

//        public void delete(list<tentity> listofentitites)
//        {
//            dbcontext.set<tentity>().removerange(listofentitites);
//            savechanges();
//            dblogger(listofentitites, null, dboperation.delete);
//        }

//        public void delete(tentity entity)
//        {
//            int keyvalue = getprimarykeyvalue(entity);
//            var oldentity = getbyid(keyvalue);
//            dbcontext.set<tentity>().remove(entity);
//            savechanges();
//            dblogger(oldentity, null, dboperation.delete);
//        }

//        public void delete(int id)
//        {
//            var itemtodelete = dbcontext.set<tentity>().find(id);
//            if (itemtodelete != null)
//            {
//                dbcontext.set<tentity>().remove(itemtodelete);
//                savechanges();
//                dblogger(itemtodelete, null, dboperation.delete);
//            }
//        }

//        public void delete(expression<func<tentity, bool>> where)
//        {
//            var itemtodelete = dbcontext.set<tentity>().where(where).asnotracking().tolist();
//            dbcontext.set<tentity>().removerange(itemtodelete);
//            savechanges();
//            dblogger(itemtodelete, null, dboperation.delete);
//        }
//        public tentity getbyid(int id)
//        {
//            dbcontext.changetracker.querytrackingbehavior = querytrackingbehavior.notracking;
//            return dbcontext.set<tentity>().find(id);
//        }

//        //public tentity get(expression<func<tentity, bool>> where, expression<func<products, object>>[] includes)
//        //{
//        //    dbcontext.changetracker.querytrackingbehavior = querytrackingbehavior.notracking;
//        //    return dbcontext.set<tentity>().where(where).asnotracking().firstordefault();
//        //}

//        public list<tentity> getall()
//        {
//            dbcontext.changetracker.querytrackingbehavior = querytrackingbehavior.notracking;
//            return dbcontext.set<tentity>().asnotracking().tolist();
//        }

//        public ilist<tentity> getmany(expression<func<tentity, bool>> where)
//        {
//            dbcontext.changetracker.querytrackingbehavior = querytrackingbehavior.notracking;
//            return dbcontext.set<tentity>().where(where).asnotracking().tolist();
//        }

//        public list<tentity> getmany(expression<func<tentity, bool>> filter = null,
//            func<iqueryable<tentity>, iorderedqueryable<tentity>> orderby = null,
//            params expression<func<tentity, object>>[] includes)
//        {
//            var dbset = dbcontext.set<tentity>();
//            iqueryable<tentity> query = dbset;

//            foreach (expression<func<tentity, object>> include in includes)
//                query = query.include(include);

//            if (filter != null)
//                query = query.where(filter);

//            if (orderby != null)
//                query = orderby(query);

//            return query.asnotracking().tolist();
//        }

//        public ilist<tentity> getpage(expression<func<tentity, bool>> where, int pagesize)
//        {
//            throw new notimplementedexception();
//        }
//        #endregion
//        public int getcount()
//        {
//            dbcontext.changetracker.querytrackingbehavior = querytrackingbehavior.notracking;
//            return dbcontext.set<tentity>().count();
//        }

//        public int getcount(expression<func<tentity, bool>> where, expression<func<tentity, object>>[] includes)
//        {
//            dbcontext.changetracker.querytrackingbehavior = querytrackingbehavior.notracking;
//            return dbcontext.set<tentity>().count(where);
//        }
//        public int getcount(expression<func<tentity, bool>> where)
//        {
//            dbcontext.changetracker.querytrackingbehavior = querytrackingbehavior.notracking;
//            return dbcontext.set<tentity>().count(where);
//        }
//        public tentity get(expression<func<tentity, bool>> where, expression<func<tentity, object>>[] includes)
//        {
//            dbcontext.changetracker.querytrackingbehavior = querytrackingbehavior.notracking;
//            var dbset = dbcontext.set<tentity>();
//            iqueryable<tentity> query = dbset;
//            foreach (expression<func<tentity, object>> include in includes)
//                query = query.include(include);
//            return query.where(where).asnotracking().firstordefault();
//        }

//        public tentity get(expression<func<tentity, bool>> where)
//        {
//            dbcontext.changetracker.querytrackingbehavior = querytrackingbehavior.notracking;
//            return dbcontext.set<tentity>().where(where).asnotracking().firstordefault();
//        }
//    }
//}
