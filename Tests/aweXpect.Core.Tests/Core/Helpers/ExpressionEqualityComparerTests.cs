using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using aweXpect.Core.Helpers;

namespace aweXpect.Core.Tests.Core.Helpers;

public sealed class ExpressionEqualityComparerTests
{
	public sealed class EqualsTests
	{
		[Fact]
		public async Task DifferentMembers_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Order, object> sut = new();
			Expression<Func<Order, object>> x = order => order.Number;
			Expression<Func<Order, object>> y = order => order.LineItems;
			bool e = sut.Equals(x, y);
			await That(e).IsFalse();
		}

		[Fact]
		public async Task DifferentMembersWithSameOperators_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Order, bool> sut = new();
			Expression<Func<Order, bool>> x = order => order.Customer.Address.Postcode == 5;
			Expression<Func<Order, bool>> y = order => order.Number == 5;
			bool e = sut.Equals(x, y);
			await That(e).IsFalse();
		}

		[Fact]
		public async Task DifferentNestedMembers_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Order, object> sut = new();
			Expression<Func<Order, object>> x = order => order.Customer.Address;
			Expression<Func<Order, object>> y = order => order.LineItems.Select(item => item.Product);
			bool e = sut.Equals(x, y);
			await That(e).IsFalse();
		}

		[Fact]
		public async Task DifferentOperators_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Order, bool> sut = new();
			Expression<Func<Order, bool>> x = order => order.Number < 5;
			Expression<Func<Order, bool>> y = order => order.Number > 5;
			bool e = sut.Equals(x, y);
			await That(e).IsFalse();
		}

		[Fact]
		public async Task DifferentPropertyValues_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Order, bool> sut = new();
			Expression<Func<Order, bool>> x = order => order.Customer == new Customer
			{
				Name = "john",
			};
			Expression<Func<Order, bool>> y = order => order.Customer == new Customer
			{
				Name = "paul",
			};
			bool e = sut.Equals(x, y);
			await That(e).IsFalse();
		}

		[Fact]
		public async Task DifferentPropertyValuesInVariable_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Order, bool> sut = new();
			Customer a = new()
			{
				Name = "john",
			};
			Customer b = new()
			{
				Name = "paul",
			};
			Expression<Func<Order, bool>> x = order => order.Customer == a;
			Expression<Func<Order, bool>> y = order => order.Customer == b;
			bool e = sut.Equals(x, y);
			await That(e).IsFalse();
		}

		[Fact]
		public async Task DifferentValues_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Customer, bool> sut = new();
			Expression<Func<Customer, bool>> x = customer => customer.Name == "john";
			Expression<Func<Customer, bool>> y = customer => customer.Name == "paul";
			bool e = sut.Equals(x, y);
			await That(e).IsFalse();
		}

		[Fact]
		public async Task NegatedOperators_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Order, bool> sut = new();
			Expression<Func<Order, bool>> x = order => order.Number != 5;
			Expression<Func<Order, bool>> y = order => order.Number == 5;
			bool e = sut.Equals(x, y);
			await That(e).IsFalse();
		}

		[Fact]
		public async Task SameOperatorAndValues_ShouldBeEqual()
		{
			ExpressionEqualityComparer<Order, bool> sut = new();
			Expression<Func<Order, bool>> x = order => order.Number == 5;
			Expression<Func<Order, bool>> y = order => order.Number == 5;
			bool e = sut.Equals(x, y);
			await That(e).IsTrue();
		}

		[Fact]
		public async Task SameProperty_ShouldBeEqual()
		{
			ExpressionEqualityComparer<Order, object> sut = new();
			Expression<Func<Order, object>> x = order => order.Customer.Address;
			Expression<Func<Order, object>> y = order => order.Customer.Address;
			bool e = sut.Equals(x, y);
			await That(e).IsTrue();
		}

		[Fact]
		public async Task SameVariableValues_ShouldBeEqual()
		{
			ExpressionEqualityComparer<Order, bool> sut = new();
			Customer customer = new()
			{
				Name = "john",
			};
			Expression<Func<Order, bool>> x = order => order.Customer == customer;
			Expression<Func<Order, bool>> y = order => order.Customer == customer;
			bool e = sut.Equals(x, y);
			await That(e).IsTrue();
		}
	}

	public sealed class GetHashCodeTests
	{
		[Fact]
		public async Task DifferentMembers_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Order, object> sut = new();
			Expression<Func<Order, object>> x = order => order.Number;
			Expression<Func<Order, object>> y = order => order.LineItems;
			await That(sut.GetHashCode(x)).IsNotEqualTo(sut.GetHashCode(y));
		}

		[Fact]
		public async Task DifferentMembersWithSameOperators_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Order, bool> sut = new();
			Expression<Func<Order, bool>> x = order => order.Customer.Address.Postcode == 5;
			Expression<Func<Order, bool>> y = order => order.Number == 5;
			await That(sut.GetHashCode(x)).IsNotEqualTo(sut.GetHashCode(y));
		}

		[Fact]
		public async Task DifferentNestedMembers_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Order, object> sut = new();
			Expression<Func<Order, object>> x = order => order.Customer.Address;
			Expression<Func<Order, object>> y = order => order.LineItems.Select(item => item.Product);
			await That(sut.GetHashCode(x)).IsNotEqualTo(sut.GetHashCode(y));
		}

		[Fact]
		public async Task DifferentOperators_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Order, bool> sut = new();
			Expression<Func<Order, bool>> x = order => order.Number < 5;
			Expression<Func<Order, bool>> y = order => order.Number > 5;
			await That(sut.GetHashCode(x)).IsNotEqualTo(sut.GetHashCode(y));
		}

		[Fact]
		public async Task DifferentPropertyValues_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Order, bool> sut = new();
			Expression<Func<Order, bool>> x = order => order.Customer == new Customer
			{
				Name = "john",
			};
			Expression<Func<Order, bool>> y = order => order.Customer == new Customer
			{
				Name = "paul",
			};
			await That(sut.GetHashCode(x)).IsNotEqualTo(sut.GetHashCode(y));
		}

		[Fact]
		public async Task DifferentPropertyValuesInVariable_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Order, bool> sut = new();
			Customer a = new()
			{
				Name = "john",
			};
			Customer b = new()
			{
				Name = "paul",
			};
			Expression<Func<Order, bool>> x = order => order.Customer == a;
			Expression<Func<Order, bool>> y = order => order.Customer == b;
			await That(sut.GetHashCode(x)).IsNotEqualTo(sut.GetHashCode(y));
		}

		[Fact]
		public async Task DifferentValues_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Customer, bool> sut = new();
			Expression<Func<Customer, bool>> x = customer => customer.Name == "john";
			Expression<Func<Customer, bool>> y = customer => customer.Name == "paul";
			await That(sut.GetHashCode(x)).IsNotEqualTo(sut.GetHashCode(y));
		}

		[Fact]
		public async Task NegatedOperators_ShouldNotBeEqual()
		{
			ExpressionEqualityComparer<Order, bool> sut = new();
			Expression<Func<Order, bool>> x = order => order.Number != 5;
			Expression<Func<Order, bool>> y = order => order.Number == 5;
			await That(sut.GetHashCode(x)).IsNotEqualTo(sut.GetHashCode(y));
		}

		[Fact]
		public async Task SameOperatorAndValues_ShouldBeEqual()
		{
			ExpressionEqualityComparer<Order, bool> sut = new();
			Expression<Func<Order, bool>> x = order => order.Customer == new Customer
			{
				Name = "john",
			};
			Expression<Func<Order, bool>> y = order => order.Customer == new Customer
			{
				Name = "john",
			};
			await That(sut.GetHashCode(x)).IsEqualTo(sut.GetHashCode(y));
		}

		[Fact]
		public async Task SameProperty_ShouldBeEqual()
		{
			ExpressionEqualityComparer<Order, object> sut = new();
			Expression<Func<Order, object>> x = order => order.Customer.Address;
			Expression<Func<Order, object>> y = order => order.Customer.Address;
			await That(sut.GetHashCode(x)).IsEqualTo(sut.GetHashCode(y));
		}

		[Fact]
		public async Task SameVariableValues_ShouldBeEqual()
		{
			ExpressionEqualityComparer<Order, bool> sut = new();
			Expression<Func<Order, bool>> x = order => order.Number == 5;
			Expression<Func<Order, bool>> y = order => order.Number == 5;
			await That(sut.GetHashCode(x)).IsEqualTo(sut.GetHashCode(y));
		}
	}

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	// ReSharper disable UnusedMember.Global
	// ReSharper disable UnusedAutoPropertyAccessor.Global
	// ReSharper disable NonReadonlyMemberInGetHashCode
	public class Product
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
	}

	public class OrderLineItem
	{
		public Product Product { get; set; }
		public int Quantity { get; set; }
	}

	public class Order
	{
		public int Number { get; set; }
		public Customer Customer { get; set; }

		public IEnumerable<OrderLineItem> LineItems { get; set; }
	}

	public class Customer
	{
		public string Name { get; set; }
		public Address Address { get; set; }

		public override bool Equals(object? obj)
		{
			Customer? other = obj as Customer;
			if (other == null)
			{
				return false;
			}

			return Name.Equals(other.Name) && Address.Equals(other.Address);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = (hash * 23) + Name.GetHashCode();
				hash = (hash * 23) + Address.GetHashCode();
				return hash;
			}
		}
	}

	public class Address
	{
		public string Suburb { get; set; }
		public int Postcode { get; set; }

		public override bool Equals(object? obj)
		{
			Address? other = obj as Address;
			if (other == null)
			{
				return false;
			}

			return Suburb.Equals(other.Suburb) && Postcode.Equals(other.Postcode);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = (hash * 23) + Suburb.GetHashCode();
				hash = (hash * 23) + Postcode.GetHashCode();
				return hash;
			}
		}
	}
	// ReSharper restore NonReadonlyMemberInGetHashCode
	// ReSharper restore UnusedAutoPropertyAccessor.Global
	// ReSharper restore UnusedMember.Global
#pragma warning restore CS8618
}
