using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonDirectory.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private Entities context = new Entities();
        private GenericRepository<Person> personRepository;
        private GenericRepository<City> cityRepository;
        private GenericRepository<Phone> phoneRepository;
        private GenericRepository<PhoneType> phoneTypeRepository;
        private GenericRepository<PersonConnection> connectionRepository;
        private GenericRepository<ConnectionType> connectionTypeRepository;
        private GenericRepository<Gender> genderRepository;
        private GenericRepository<ErrorLog> errorLogsRepository;
        public GenericRepository<Person> PersonRepository
        {
            get
            {

                if (this.personRepository == null)
                {
                    this.personRepository = new GenericRepository<Person>(context);
                }
                return personRepository;
            }
        }

        public GenericRepository<City> CityRepository
        {
            get
            {

                if (this.cityRepository == null)
                {
                    this.cityRepository = new GenericRepository<City>(context);
                }
                return cityRepository;
            }
        }
        public GenericRepository<Phone> PhoneRepository
        {
            get
            {

                if (this.phoneRepository == null)
                {
                    this.phoneRepository = new GenericRepository<Phone>(context);
                }
                return phoneRepository;
            }
        }
        public GenericRepository<PhoneType> PhoneTypeRepository
        {
            get
            {

                if (this.phoneTypeRepository == null)
                {
                    this.phoneTypeRepository = new GenericRepository<PhoneType>(context);
                }
                return phoneTypeRepository;
            }
        }
        public GenericRepository<PersonConnection> ConnectionRepository
        {
            get
            {

                if (this.connectionRepository == null)
                {
                    this.connectionRepository = new GenericRepository<PersonConnection>(context);
                }
                return connectionRepository;
            }
        }
        public GenericRepository<ConnectionType> ConnectionTypeRepository
        {
            get
            {

                if (this.connectionTypeRepository == null)
                {
                    this.connectionTypeRepository = new GenericRepository<ConnectionType>(context);
                }
                return connectionTypeRepository;
            }
        }

        public GenericRepository<Gender> GenderRepository
        {
            get
            {

                if (this.genderRepository == null)
                {
                    this.genderRepository = new GenericRepository<Gender>(context);
                }
                return genderRepository;
            }
        }
        public GenericRepository<ErrorLog> ErrorLogsRepository
        {
            get
            {

                if (this.errorLogsRepository == null)
                {
                    this.errorLogsRepository = new GenericRepository<ErrorLog>(context);
                }
                return errorLogsRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
