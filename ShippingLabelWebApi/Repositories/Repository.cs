
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ShippingLabelWebApi.Repositories
//{
//    public class Repository<T> where T : class
//    {
//        private RepositoryBase<T> _repository;

//        /// <summary>
//        /// generic single property to read data from repositories.
//        /// </summary>
//        //public RepositoryBase<T> DataSet =>
//        //    _repository ?? (_repository = Activator.CreateInstance(typeof(RepositoryBase<T>)) as RepositoryBase<T>);
//        public RepositoryBase<T> DataSet
//        {
//            get
//            {
//                return _repository = Activator.CreateInstance(typeof(RepositoryBase<T>)) as RepositoryBase<T>;
//            }
//        }
//    }
//}
