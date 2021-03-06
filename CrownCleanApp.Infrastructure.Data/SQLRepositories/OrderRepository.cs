﻿using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.DomainService.Filtering;
using CrownCleanApp.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrownCleanApp.Infrastructure.Data.SQLRepositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly CrownCleanAppContext _ctx;

        public OrderRepository(CrownCleanAppContext ctx)
        {
            _ctx = ctx;
        }

        public Order Create(Order order)
        {
            _ctx.Attach(order).State = EntityState.Added;
            _ctx.SaveChanges();
            return order;
        }

        public Order Delete(int id)
        {
            Order order = _ctx.Orders.Remove(new Order { ID = id }).Entity;
            _ctx.SaveChanges();
            return order;
        }

        public FilteredList<Order> ReadAll(OrderFilter filter = null)
        {
            FilteredList<Order> filteredList = new FilteredList<Order>();


            if (filter != null)
            {
                #region Filtering

                filteredList.List = _ctx.Orders;

                if (filter.UserID > 0)
                {
                    filteredList.List = filteredList.List.Where(o => o.UserID == filter.UserID);
                }

                if (filter.UserID > 0)
                {
                    filteredList.List = filteredList.List.Where(o => o.UserID == filter.UserID);
                }

                if (!string.IsNullOrEmpty(filter.ServicesSearch))
                {
                    filteredList.List = filteredList.List.Where(o => o.Services.Contains(filter.ServicesSearch));
                }

                if (!string.IsNullOrEmpty(filter.DescriptionSearch))
                {
                    filteredList.List = filteredList.List.Where(o => o.Description.Contains(filter.DescriptionSearch));
                }

                #endregion

                #region Pagination
                if (filter.CurrentPage > 0 && filter.ItemsPerPage > 0)
                {
                    filteredList.List = filteredList.List
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPerPage)
                    .Take(filter.ItemsPerPage);
                }

                filteredList.Count = _ctx.Orders.Count();

                #endregion 
            }
            else
            {
                filteredList.List = _ctx.Orders;
                filteredList.Count = _ctx.Orders.Count();
            }

            return filteredList;
        }

        public Order ReadByID(int id)
        {
            return _ctx.Orders.Include(o => o.Vehicle).Include(o => o.User).AsNoTracking().FirstOrDefault(o => o.ID == id);
        }

        public Order Update(Order order)
        {
            _ctx.Attach(order).State = EntityState.Modified;
            _ctx.Entry(order).Reference(o => o.User).IsModified = true;
            _ctx.Entry(order).Reference(o => o.Vehicle).IsModified = true;
            _ctx.SaveChanges();
            return order;
        }
    }
}
