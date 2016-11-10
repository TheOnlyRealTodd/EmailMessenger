using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ContactFormWithEmail.Models
{


    

    public class MessageRepository :IRepository<Message>
    {


        public MessageRepository()
        {
            
        }
        public Message Get(int id)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                return _context.Messages.Find(id);
            }
            
        }

        public IList<Message> GetAll()
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                return _context.Messages.ToList();
            }
        }
        public Message SingleOrDefault(Expression<Func<Message, bool>> predicate)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                return _context.Messages.SingleOrDefault(predicate);
            }
        }

        public void Add(Message message)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                _context.Messages.Add(message);
                _context.SaveChanges();
            }
        }

        public IList<Message> FindAny(Expression<Func<Message, bool>> predicate)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                return _context.Set<Message>().Where(predicate).ToList();
            }
        }


        public void PersistChanges()
        {
            //Standby
        }
    }
}