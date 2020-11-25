﻿using Edument.CQRS;
using Cafe.Events;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Cafe.Domain.Commands;
using Cafe.Domain.Exceptions;

namespace Cafe.Tests
{
    [TestFixture]
    public class TabTests : BDDTest<TabAggregate>
    {
        private Guid testId;
        private int testTable;
        private string testWaiter;
        private OrderedItem testDrink1;
        private OrderedItem testDrink2;
        private OrderedItem testFood1;

        [SetUp]
        public void Setup()
        {
            testId = Guid.NewGuid();
            testTable = 42;
            testWaiter = "Derek";
            testDrink1 = new OrderedItem { IsDrink = true, Description = "Beer", Price = 3, MenuNumber = 1 };
            testDrink2 = new OrderedItem { IsDrink = true, Description = "Wine", Price = 5, MenuNumber = 2 };
            testFood1 = new OrderedItem { IsDrink = false, Description = "Burger", Price = 11, MenuNumber = 3 };
        }

        [Test]
        public void CanOpenANewTab()
        {
            Test(
                Given(),
                When(new OpenTab
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                }),
                Then(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                }));
        }

        [Test]
        public void CanNotOrderWithUnopenedTab()
        {
            Test(
                Given(),
                When(new PlaceOrder
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testDrink1 }
                }),
                ThenFailWith<TabNotOpen>());
        }

        [Test]
        public void CanPlaceDrinksOrder()
        {
            Test(
                Given(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                }),
                When(new PlaceOrder
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testDrink1, testDrink2 }
                }),
                Then(new DrinksOrdered
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testDrink1, testDrink2 }
                }));
        }

        [Test]
        public void CanPlaceFoodOrder()
        {
            Test(
                Given(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                }),
                When(new PlaceOrder
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testFood1, testFood1 }
                }),
                Then(new FoodOrdered
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testFood1, testFood1 }
                }));
        }

        [Test]
        public void CanPlaceFoodAndDrinkOrder()
        {
            Test(
                Given(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                }),
                When(new PlaceOrder
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testFood1, testDrink2 }
                }),
                Then(new DrinksOrdered
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testDrink2 }
                },
                new FoodOrdered
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testFood1 }
                }));
        }

        [Test]
        public void OrderedDrinksCanBeServed()
        {
            Test(
                Given(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                },
                new DrinksOrdered
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testDrink1, testDrink2 }
                }),
                When(new MarkDrinksServed
                {
                    Id = testId,
                    MenuNumbers = new List<int>
                        { testDrink1.MenuNumber, testDrink2.MenuNumber }
                }),
                Then(new DrinksServed
                {
                    Id = testId,
                    MenuNumbers = new List<int>
                        { testDrink1.MenuNumber, testDrink2.MenuNumber }
                }));
        }

        [Test]
        public void CanNotServeAnUnorderedDrink()
        {
            Test(
                Given(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                },
                new DrinksOrdered
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testDrink1 }
                }),
                When(new MarkDrinksServed
                {
                    Id = testId,
                    MenuNumbers = new List<int> { testDrink2.MenuNumber }
                }),
                ThenFailWith<DrinksNotOutstanding>());
        }

        [Test]
        public void CanNotServeAnOrderedDrinkTwice()
        {
            Test(
                Given(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                },
                new DrinksOrdered
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testDrink1 }
                },
                new DrinksServed
                {
                    Id = testId,
                    MenuNumbers = new List<int> { testDrink1.MenuNumber }
                }),
                When(new MarkDrinksServed
                {
                    Id = testId,
                    MenuNumbers = new List<int> { testDrink1.MenuNumber }
                }),
                ThenFailWith<DrinksNotOutstanding>());
        }

        [Test]
        public void CanCloseTabWithTip()
        {
            Test(
                Given(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                },
                new DrinksOrdered
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testDrink2 }
                },
                new DrinksServed
                {
                    Id = testId,
                    MenuNumbers = new List<int> { testDrink2.MenuNumber }
                }),
                When(new CloseTab
                {
                    Id = testId,
                    AmountPaid = testDrink2.Price + 0.50M
                }),
                Then(new TabClosed
                {
                    Id = testId,
                    AmountPaid = testDrink2.Price + 0.50M,
                    OrderValue = testDrink2.Price,
                    TipValue = 0.50M
                }));
        }
    }
}
