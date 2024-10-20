// See https://aka.ms/new-console-template for more information
using Captinslog.Application;
using Captinslog.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var ioc = new ServiceCollection()
    .AddLogging(opt =>
    {
    })
    .AddCaptinslogApp()
    .AddLogEntryRepository()
    .BuildServiceProvider();

var service = ioc.GetRequiredService<ILogEntryService>();
service.Add(Book.MainProgramTest,"starting","consoleApp");
service.Add(Book.MainProgramTest, "ending", "consoleApp");

var invoice = new Invoice
{
    InvoiceNumber = "123",
    Amount = 100.00m
};

service.Add(Book.SendInvoice, "send invoice");
service.Add(Book.SendInvoice, "building invoice");
service.Add(Book.SendInvoice, "done invoice", invoice);



public class Invoice
{
    public string InvoiceNumber { get; set; }
    public decimal Amount { get; set; }
}

public static class Book
{
    public static Guid MainProgramTest => Guid.NewGuid();

    public static Guid SendInvoice => Guid.NewGuid();
}