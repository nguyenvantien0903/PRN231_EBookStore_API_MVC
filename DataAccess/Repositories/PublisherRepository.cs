using BusinessObject;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        public void DeletePublisher(Publisher p) => PublisherDAO.DeletePublisher(p);

        public Publisher GetPublisherById(int id) => PublisherDAO.FindPublisherById(id);

        public List<Publisher> GetPublishers() => PublisherDAO.GetPublishers();

        public void SavePublisher(Publisher p) => PublisherDAO.SavePublisher(p);

        public void UpdatePublisher(Publisher p) => PublisherDAO.UpdatePublisher(p);
    }
}
