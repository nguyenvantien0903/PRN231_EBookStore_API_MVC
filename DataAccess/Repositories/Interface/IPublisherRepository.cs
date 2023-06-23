using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interface
{
    public interface IPublisherRepository
    {
        void SavePublisher(Publisher p);

        Publisher GetPublisherById(int id);

        void DeletePublisher(Publisher p);

        void UpdatePublisher(Publisher p);

        List<Publisher> GetPublishers();
    }
}
