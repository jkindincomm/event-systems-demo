using System;
using FileHelpers;

[DelimitedRecord(",")]
public class Order
{
    public int OrderID;

    public string CustomerID;

    [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
    public DateTime OrderDate;

    [FieldConverter(ConverterKind.Decimal, ".")] // The decimal separator is .
    public decimal Freight;

}