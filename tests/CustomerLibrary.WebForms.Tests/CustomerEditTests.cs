using System;
using System.Collections.Generic;
using CustomerLibrary.BusinessLogic;
using Moq;
using Xunit;

namespace CustomerLibrary.WebForms.Tests
{
    public class CustomerEditTests
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomerEdit()
        {
            var customerEdit = new CustomerEdit();

            Assert.NotNull(customerEdit);
        }

        [Fact]
        public void ShouldCreateCustomer()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();

            var customerEdit = new CustomerEdit(customerServiceMock.Object);
            customerEdit.Customer = new Customer();

            customerEdit.SaveCustomer();

            customerServiceMock.Verify(s => s.Create(It.IsAny<Customer>()));
        }

        [Fact]
        public void ShouldUpdateCustomer()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();

            var customerEdit = new CustomerEdit(customerServiceMock.Object);
            customerEdit.Customer = new Customer {CustomerId = 10};

            customerEdit.SaveCustomer();

            customerServiceMock.Verify(s => s.Update(It.IsAny<Customer>()));
        }

        [Fact]
        public void ShouldAddNewAddressToCustomer()
        {
            var customerEdit = new CustomerEdit();
            customerEdit.Customer = new Customer {Addresses = new List<Address>()};

            customerEdit.AddAddress(this, EventArgs.Empty);

            Assert.Single(customerEdit.Customer.Addresses);
        }

        [Fact]
        public void ShouldRemoveLastAddressFromCustomer()
        {
            var customerEdit = new CustomerEdit();
            customerEdit.Customer = new Customer
                {Addresses = new List<Address> {new Address() {AddressId = 1}, new Address() {AddressId = 2}}};
            customerEdit.AddressesStartLength = 1;

            customerEdit.DeleteAddress(this, EventArgs.Empty);

            Assert.Single(customerEdit.Customer.Addresses);
            Assert.Equal(1, customerEdit.Customer.Addresses[0].AddressId);
        }

        [Fact]
        public void ShouldNotRemoveLastAddressFromCustomer()
        {
            var customerEdit = new CustomerEdit();
            customerEdit.Customer = new Customer
                {Addresses = new List<Address> {new Address() {AddressId = 1}}};
            customerEdit.AddressesStartLength = 1;

            customerEdit.DeleteAddress(this, EventArgs.Empty);

            Assert.Single(customerEdit.Customer.Addresses);
            Assert.Equal(1, customerEdit.Customer.Addresses[0].AddressId);
        }

        [Fact]
        public void ShouldAddNewNoteToCustomer()
        {
            var customerEdit = new CustomerEdit();
            customerEdit.Customer = new Customer {Notes = new List<Note>()};

            customerEdit.AddNote(this, EventArgs.Empty);

            Assert.Single(customerEdit.Customer.Notes);
        }

        [Fact]
        public void ShouldRemoveLastNoteFromCustomer()
        {
            var customerEdit = new CustomerEdit();
            customerEdit.Customer = new Customer
                {Notes = new List<Note> {new Note() {NoteId = 1}, new Note() {NoteId = 2}}};
            customerEdit.NotesStartLength = 1;

            customerEdit.DeleteNote(this, EventArgs.Empty);

            Assert.Single(customerEdit.Customer.Notes);
            Assert.Equal(1, customerEdit.Customer.Notes[0].NoteId);
        }

        [Fact]
        public void ShouldNotRemoveLastNoteFromCustomerIfItIsOnlyAddress()
        {
            var customerEdit = new CustomerEdit();
            customerEdit.Customer = new Customer
                {Notes = new List<Note> {new Note() {NoteId = 1}}};
            customerEdit.NotesStartLength = 1;

            customerEdit.DeleteNote(this, EventArgs.Empty);

            Assert.Single(customerEdit.Customer.Notes);
            Assert.Equal(1, customerEdit.Customer.Notes[0].NoteId);
        }
    }
}