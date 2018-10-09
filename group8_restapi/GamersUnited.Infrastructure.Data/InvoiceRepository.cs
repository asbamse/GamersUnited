using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;
using GamersUnited.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamersUnited.Infrastructure.Data
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        readonly GamersUnitedContext _ctx;
        readonly IRepository<ProductCategory> _pcr;

        public InvoiceRepository(GamersUnitedContext context, IRepository<ProductCategory> productCategoryRepository)
        {
            _ctx = context;
            _pcr = productCategoryRepository;
        }

        public Invoice Add(Invoice obj)
        {
            if (obj.Products == null)
            {
                throw new ArgumentNullException("The products cannot be null");
            }
            else if (obj.Customer == null)
            {
                throw new ArgumentNullException("The customer cannot be null");
            }
            else if (obj.DateSold == null)
            {
                throw new ArgumentNullException("The sold date cannot be null");
            }

            Invoice item;
            
            var entityEntry = _ctx.Attach(obj);
            entityEntry.State = EntityState.Added;
            item = entityEntry.Entity;
            _ctx.SaveChanges();

            return item;
        }

        public int Count()
        {
            return _ctx.Invoice.Count();
        }

        public IList<Invoice> GetAll()
        {
            return _ctx.Invoice.Include(i => i.Products).Include(i => i.Customer).ToList();
        }

        public Invoice GetById(int id)
        {
            var item = _ctx.Invoice.Include(i => i.Products).Include(i => i.Customer).FirstOrDefault(b => b.Id == id);

            if (item == null)
            {
                throw new ArgumentOutOfRangeException("Id not found!");
            }

            return item;
        }

        public Invoice Remove(Invoice obj)
        {
            var item = GetById(obj.Id);

            var entityEntry = _ctx.Invoice.Attach(item);
            entityEntry.State = EntityState.Deleted;
            _ctx.SaveChanges();

            return entityEntry.Entity;
        }

        public Invoice Update(int id, Invoice obj)
        {
            if (obj.Products == null)
            {
                throw new ArgumentNullException("The products cannot be null");
            }
            else if (obj.Customer == null)
            {
                throw new ArgumentNullException("The customer cannot be null");
            }
            else if (obj.DateSold == null)
            {
                throw new ArgumentNullException("The sold date cannot be null");
            }
            
            List<SoldInvoiceRelation> soldInvoices = new List<SoldInvoiceRelation>(obj.Products);

            // Deletes the Invoice products which is already in the sold invoice relation.
            var productsFromDb = _ctx.Invoice.Include(i => i.Products).AsNoTracking().FirstOrDefault(i => i.Id == obj.Id).Products.ToList();

            foreach (var invoiceSold in productsFromDb)
            {
                if (soldInvoices.Exists(i => i.SoldId == invoiceSold.SoldId))
                {
                    _ctx.Entry(invoiceSold).State = EntityState.Unchanged;
                    soldInvoices.RemoveAll(i => i.SoldId == invoiceSold.SoldId);
                }
                else if (!soldInvoices.Exists(i => i.SoldId == invoiceSold.SoldId))
                {
                    _ctx.Entry(invoiceSold).State = EntityState.Deleted;
                }
            }

            obj.Products = soldInvoices;

            _ctx.Attach(obj);
            _ctx.Attach(obj).State = EntityState.Modified;

            _ctx.Entry(obj).Collection(i => i.Products).IsModified = false;
            _ctx.Entry(obj).Reference(i => i.Customer).IsModified = true;

            _ctx.SaveChanges();

            return _ctx.Invoice.Include(i => i.Products).Include(i => i.Customer).FirstOrDefault(i => i.Id == obj.Id);
        }
    }
}
