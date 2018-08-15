namespace Stations.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Stations.Models;

    public class QueryHelper
    {
        private readonly StationsContext context;

        public QueryHelper(StationsContext context)
        {
            this.context = context;
        }

        public void AddRange<T>(IEnumerable<T> entities) where T : class
        {
            this.context.Set<T>().AddRange(entities);
            this.context.SaveChanges();
        }

        public SeatingClass GetOrAddSeatingClass(SeatingClass seatingClass, out bool isImported)
        {
            SeatingClass seat = this.context.SeatingClasses.SingleOrDefault(sc => sc.Name == seatingClass.Name);
            isImported = false;
            if (seat == null)
            {
                seat = seatingClass;
                isImported = true;

                if (!this.IsEntityValid(seat))
                {
                    throw new InvalidOperationException("Seat is not valid!");
                }

                this.context.SeatingClasses.Add(seat);
                this.context.SaveChanges();
            }

            return seat;
        }

        public bool IsEntityValid<T>(T entity) where T : class
        {
            return this.context
                .Entry(entity)
                .GetValidationResult()
                .IsValid;
        }

        public bool IsEntityExisting<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return this.context.Set<T>().Any(predicate);
        }

        public T SingleOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return this.context.Set<T>().SingleOrDefault(predicate);
        }

        public IQueryable<T> Filter<T>(Expression<Func<T, bool>> filter) where T : class
        {
            return this.context.Set<T>().Where(filter).AsQueryable();
        }
    }
}
