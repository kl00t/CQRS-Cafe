using Edument.CQRS;
using Events.Cafe;
using System.Collections;
using System.Linq;
using YourDomain.Exceptions;

namespace YourDomain.Commands
{
    public class TabAggregate : Aggregate, 
        IHandleCommand<OpenTab>, 
        IHandleCommand<PlaceOrder>,
        IHandleCommand<MarkDrinksServed>,
        IApplyEvent<TabOpened>,
        IApplyEvent<DrinksOrdered>
    {
        private bool open = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void Apply(TabOpened e)
        {
            open = true;   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void Apply(DrinksOrdered e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public IEnumerable Handle(OpenTab c)
        {
            yield return new TabOpened
            {
                Id = c.Id,
                TableNumber = c.TableNumber,
                Waiter = c.Waiter
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public IEnumerable Handle(PlaceOrder c)
        {
            if (!open)
            {
                throw new TabNotOpen();
            }

            var drink = c.Items.Where(i => i.IsDrink).ToList();
            if (drink.Any())
            {
                yield return new DrinksOrdered
                {
                    Id = c.Id,
                    Items = drink
                };
            }

            var food = c.Items.Where(i => !i.IsDrink).ToList();
            if (food.Any())
            {
                yield return new FoodOrdered
                {
                    Id = c.Id,
                    Items = food
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public IEnumerable Handle(MarkDrinksServed c)
        {
            yield return new DrinksServed
            {
                Id = c.Id,
                MenuNumbers = c.MenuNumbers
            };
        }
    }
}
