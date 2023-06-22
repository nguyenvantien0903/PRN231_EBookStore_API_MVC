using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class PublisherDAO
    {
        public static List<Publisher> GetPublishers()
        {
            var listPublishers = new List<Publisher>();
            try
            {
                using var context = new eBookStoreDBContext();
                listPublishers = context.Publishers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listPublishers;
        }

        public static Publisher FindPublisherById(int prodId)
        {
            Publisher p = new();
            try
            {
                using var context = new eBookStoreDBContext();
                p = context.Publishers.SingleOrDefault(x => x.PublisherId == prodId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }

        public static void SavePublisher(Publisher Publisher)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                context.Publishers.Add(Publisher);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdatePublisher(Publisher Publisher)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                context.Entry<Publisher>(Publisher).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeletePublisher(Publisher Publisher)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                var p1 = context.Publishers.SingleOrDefault(
                    x => x.PublisherId == Publisher.PublisherId);
                context.Publishers.Remove(p1);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
