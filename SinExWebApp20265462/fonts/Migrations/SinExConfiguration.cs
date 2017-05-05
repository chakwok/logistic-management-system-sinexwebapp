namespace SinExWebApp20265462.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class SinExConfiguration : DbMigrationsConfiguration<SinExWebApp20265462.Models.SinExDatabaseContext>
    {
        public SinExConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SinExWebApp20265462.Models.SinExDatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.PackageTypes.AddOrUpdate(
                p => p.PackageTypeID,
                new PackageType { PackageTypeID = 1, Type = "Envelope", Description = "for correspondence and documents only with no commercial value" },
                new PackageType { PackageTypeID = 2, Type = "Pak", Description = "for flat, non-breakable articles including heavy documents" },
                new PackageType { PackageTypeID = 3, Type = "Tube", Description = "for larger documents, such as blueprints, which should be rolled rather than folded" },
                new PackageType { PackageTypeID = 4, Type = "Box", Description = "for bulky items, such as electronic parts and textile samples" },
                new PackageType { PackageTypeID = 5, Type = "Customer", Description = "for packaging provided by customer" }
            );

            context.PackageTypeSizes.AddOrUpdate(
                p => p.PackageTypeSizeID,
                new PackageTypeSize { PackageTypeSizeID = 1, Size = "250x350mm", WeightLimit = 0, PackageTypeID = 1 },
                new PackageTypeSize { PackageTypeSizeID = 2, Size = "small - 350x400mm", WeightLimit = 5, PackageTypeID = 2 },
                new PackageTypeSize { PackageTypeSizeID = 3, Size = "big - 450x550mm", WeightLimit = 5, PackageTypeID = 2 },
                new PackageTypeSize { PackageTypeSizeID = 4, Size = "1000x80mm", WeightLimit = 0, PackageTypeID = 3 },
                new PackageTypeSize { PackageTypeSizeID = 5, Size = "small - 300x250x150mm", WeightLimit = 10, PackageTypeID = 4 },
                new PackageTypeSize { PackageTypeSizeID = 6, Size = "medium - 400x350x250mm", WeightLimit = 20, PackageTypeID = 4 },
                new PackageTypeSize { PackageTypeSizeID = 7, Size = "big - 500x450x350mm", WeightLimit = 30, PackageTypeID = 4 },
                new PackageTypeSize { PackageTypeSizeID = 8, Size = "Custom Size", WeightLimit = -1, PackageTypeID = 5 }
            );

            // Add service type data.
            context.ServiceTypes.AddOrUpdate(
                p => p.ServiceTypeID,
                new ServiceType { ServiceTypeID = 1, Type = "Same Day", CutoffTime = "10:00 a.m.", DeliveryTime = "Same day" },
                new ServiceType { ServiceTypeID = 2, Type = "Next Day 10:30", CutoffTime = "3:00 p.m.", DeliveryTime = "Next day 10:30 a.m." },
                new ServiceType { ServiceTypeID = 3, Type = "Next Day 12:00", CutoffTime = "6:00 p.m.", DeliveryTime = "Next day 12:00 p.m." },
                new ServiceType { ServiceTypeID = 4, Type = "Next Day 15:00", CutoffTime = "9:00 p.m.", DeliveryTime = "Next day 15:00 p.m." },
                new ServiceType { ServiceTypeID = 5, Type = "2nd Day", CutoffTime = "", DeliveryTime = "5:00 p.m. second business day" },
                new ServiceType { ServiceTypeID = 6, Type = "Ground", CutoffTime = "", DeliveryTime = "1 to 5 business days" }
                );

            // Add service and package type fees.
            context.ServicePackageFees.AddOrUpdate(
                p => p.ServicePackageFeeID,
                // Same Day
                new ServicePackageFee { ServicePackageFeeID = 1, Fee = 160, MinimumFee = 160, ServiceTypeID = 1, PackageTypeID = 1 }, // Envelope
                new ServicePackageFee { ServicePackageFeeID = 2, Fee = 100, MinimumFee = 160, ServiceTypeID = 1, PackageTypeID = 2 }, // Pak
                new ServicePackageFee { ServicePackageFeeID = 3, Fee = 100, MinimumFee = 160, ServiceTypeID = 1, PackageTypeID = 3 }, // Tube
                new ServicePackageFee { ServicePackageFeeID = 4, Fee = 110, MinimumFee = 160, ServiceTypeID = 1, PackageTypeID = 4 }, // Box
                new ServicePackageFee { ServicePackageFeeID = 5, Fee = 110, MinimumFee = 160, ServiceTypeID = 1, PackageTypeID = 5 }, // Customer
                                                                                                                                      // Next Day 10:30
                new ServicePackageFee { ServicePackageFeeID = 6, Fee = 140, MinimumFee = 140, ServiceTypeID = 2, PackageTypeID = 1 }, // Envelope
                new ServicePackageFee { ServicePackageFeeID = 7, Fee = 90, MinimumFee = 140, ServiceTypeID = 2, PackageTypeID = 2 }, // Pak
                new ServicePackageFee { ServicePackageFeeID = 8, Fee = 90, MinimumFee = 140, ServiceTypeID = 2, PackageTypeID = 3 }, // Tube
                new ServicePackageFee { ServicePackageFeeID = 9, Fee = 99, MinimumFee = 100, ServiceTypeID = 2, PackageTypeID = 4 }, // Box
                new ServicePackageFee { ServicePackageFeeID = 10, Fee = 99, MinimumFee = 140, ServiceTypeID = 2, PackageTypeID = 5 }, // Customer
                                                                                                                                      // Next Day 12:00
                new ServicePackageFee { ServicePackageFeeID = 11, Fee = 130, MinimumFee = 130, ServiceTypeID = 3, PackageTypeID = 1 }, // Envelope
                new ServicePackageFee { ServicePackageFeeID = 12, Fee = 80, MinimumFee = 130, ServiceTypeID = 3, PackageTypeID = 2 }, // Pak
                new ServicePackageFee { ServicePackageFeeID = 13, Fee = 80, MinimumFee = 130, ServiceTypeID = 3, PackageTypeID = 3 }, // Tube
                new ServicePackageFee { ServicePackageFeeID = 14, Fee = 88, MinimumFee = 130, ServiceTypeID = 3, PackageTypeID = 4 }, // Box
                new ServicePackageFee { ServicePackageFeeID = 15, Fee = 88, MinimumFee = 130, ServiceTypeID = 3, PackageTypeID = 5 }, // Customer
                                                                                                                                      // Next Day 15:00
                new ServicePackageFee { ServicePackageFeeID = 16, Fee = 120, MinimumFee = 120, ServiceTypeID = 4, PackageTypeID = 1 }, // Envelope
                new ServicePackageFee { ServicePackageFeeID = 17, Fee = 70, MinimumFee = 120, ServiceTypeID = 4, PackageTypeID = 2 }, // Pak
                new ServicePackageFee { ServicePackageFeeID = 18, Fee = 70, MinimumFee = 120, ServiceTypeID = 4, PackageTypeID = 3 }, // Tube
                new ServicePackageFee { ServicePackageFeeID = 19, Fee = 77, MinimumFee = 120, ServiceTypeID = 4, PackageTypeID = 4 }, // Box
                new ServicePackageFee { ServicePackageFeeID = 20, Fee = 77, MinimumFee = 120, ServiceTypeID = 4, PackageTypeID = 5 }, // Customer
                                                                                                                                      // 2nd Day
                new ServicePackageFee { ServicePackageFeeID = 21, Fee = 50, MinimumFee = 50, ServiceTypeID = 5, PackageTypeID = 1 }, // Envelope
                new ServicePackageFee { ServicePackageFeeID = 22, Fee = 50, MinimumFee = 50, ServiceTypeID = 5, PackageTypeID = 2 }, // Pak
                new ServicePackageFee { ServicePackageFeeID = 23, Fee = 50, MinimumFee = 50, ServiceTypeID = 5, PackageTypeID = 3 }, // Tube
                new ServicePackageFee { ServicePackageFeeID = 24, Fee = 55, MinimumFee = 55, ServiceTypeID = 5, PackageTypeID = 4 }, // Box
                new ServicePackageFee { ServicePackageFeeID = 25, Fee = 55, MinimumFee = 55, ServiceTypeID = 5, PackageTypeID = 5 }, // Customer
                                                                                                                                     // Ground
                new ServicePackageFee { ServicePackageFeeID = 26, Fee = 25, MinimumFee = 25, ServiceTypeID = 6, PackageTypeID = 1 },// Envelope
                new ServicePackageFee { ServicePackageFeeID = 27, Fee = 25, MinimumFee = 25, ServiceTypeID = 6, PackageTypeID = 2 }, // Pak
                new ServicePackageFee { ServicePackageFeeID = 28, Fee = 25, MinimumFee = 25, ServiceTypeID = 6, PackageTypeID = 3 }, // Tube
                new ServicePackageFee { ServicePackageFeeID = 29, Fee = 30, MinimumFee = 30, ServiceTypeID = 6, PackageTypeID = 4 }, // Box
                new ServicePackageFee { ServicePackageFeeID = 30, Fee = 30, MinimumFee = 30, ServiceTypeID = 6, PackageTypeID = 5 }  // Customer
                );

            context.Currencies.AddOrUpdate(
                p => p.CurrencyCode,
                new Currency { CurrencyCode = "CNY", ExchangeRate = 1.00 },
                new Currency { CurrencyCode = "HKD", ExchangeRate = 1.13 },
                new Currency { CurrencyCode = "MOP", ExchangeRate = 1.16 },
                new Currency { CurrencyCode = "TWD", ExchangeRate = 4.56 }
                );

            context.Destinations.AddOrUpdate(
                p => p.DestinationID,
                new Destination { DestinationID = 1, City = "Beijing", ProvinceCode = "BJ", CurrencyCode = "CNY" },
                new Destination { DestinationID = 2, City = "Changchun", ProvinceCode = "JL", CurrencyCode = "CNY" },
                new Destination { DestinationID = 3, City = "Changsha", ProvinceCode = "HN", CurrencyCode = "CNY" },
                new Destination { DestinationID = 4, City = "Chengdu", ProvinceCode = "SC", CurrencyCode = "CNY" },
                new Destination { DestinationID = 5, City = "Chongqing", ProvinceCode = "CQ", CurrencyCode = "CNY" },
                new Destination { DestinationID = 6, City = "Fuzhou", ProvinceCode = "JX", CurrencyCode = "CNY" },
                new Destination { DestinationID = 7, City = "Golmud", ProvinceCode = "QH", CurrencyCode = "CNY" },
                new Destination { DestinationID = 8, City = "Guangzhou", ProvinceCode = "GD", CurrencyCode = "CNY" },
                new Destination { DestinationID = 9, City = "Guiyang", ProvinceCode = "GZ", CurrencyCode = "CNY" },
                new Destination { DestinationID = 10, City = "Haikou", ProvinceCode = "HI", CurrencyCode = "CNY" },
                new Destination { DestinationID = 11, City = "Hailar", ProvinceCode = "NM", CurrencyCode = "CNY" },
                new Destination { DestinationID = 12, City = "Hangzhou", ProvinceCode = "ZJ", CurrencyCode = "CNY" },
                new Destination { DestinationID = 13, City = "Harbin", ProvinceCode = "HL", CurrencyCode = "CNY" },
                new Destination { DestinationID = 14, City = "Hefei", ProvinceCode = "AH", CurrencyCode = "CNY" },
                new Destination { DestinationID = 15, City = "Hohhot", ProvinceCode = "NM", CurrencyCode = "CNY" },
                new Destination { DestinationID = 16, City = "Hong Kong", ProvinceCode = "HK", CurrencyCode = "HKD" },
                new Destination { DestinationID = 17, City = "Hulun Buir", ProvinceCode = "NM", CurrencyCode = "CNY" },
                new Destination { DestinationID = 18, City = "Jinan", ProvinceCode = "SD", CurrencyCode = "CNY" },
                new Destination { DestinationID = 19, City = "Kashi", ProvinceCode = "XJ", CurrencyCode = "CNY" },
                new Destination { DestinationID = 20, City = "Kunming", ProvinceCode = "YN", CurrencyCode = "CNY" },
                new Destination { DestinationID = 21, City = "Lanzhou", ProvinceCode = "GS", CurrencyCode = "CNY" },
                new Destination { DestinationID = 22, City = "Lhasa", ProvinceCode = "XZ", CurrencyCode = "CNY" },
                new Destination { DestinationID = 23, City = "Macau", ProvinceCode = "MC", CurrencyCode = "MOP" },
                new Destination { DestinationID = 24, City = "Nanchang", ProvinceCode = "JX", CurrencyCode = "CNY" },
                new Destination { DestinationID = 25, City = "Nanjing", ProvinceCode = "JS", CurrencyCode = "CNY" },
                new Destination { DestinationID = 26, City = "Nanning", ProvinceCode = "JX", CurrencyCode = "CNY" },
                new Destination { DestinationID = 27, City = "Qiqihar", ProvinceCode = "HL", CurrencyCode = "CNY" },
                new Destination { DestinationID = 28, City = "Shanghai", ProvinceCode = "SH", CurrencyCode = "CNY" },
                new Destination { DestinationID = 29, City = "Shenyang", ProvinceCode = "LN", CurrencyCode = "CNY" },
                new Destination { DestinationID = 30, City = "Shijiazhuang", ProvinceCode = "HE", CurrencyCode = "CNY" },
                new Destination { DestinationID = 31, City = "Taipei", ProvinceCode = "TW", CurrencyCode = "TWD" },
                new Destination { DestinationID = 32, City = "Taiyuan", ProvinceCode = "SX", CurrencyCode = "CNY" },
                new Destination { DestinationID = 33, City = "Tianjin", ProvinceCode = "HE", CurrencyCode = "CNY" },
                new Destination { DestinationID = 34, City = "Urumqi", ProvinceCode = "XJ", CurrencyCode = "CNY" },
                new Destination { DestinationID = 35, City = "Wuhan", ProvinceCode = "HB", CurrencyCode = "CNY" },
                new Destination { DestinationID = 36, City = "Xi'an", ProvinceCode = "SN", CurrencyCode = "CNY" },
                new Destination { DestinationID = 37, City = "Xining", ProvinceCode = "QH", CurrencyCode = "CNY" },
                new Destination { DestinationID = 38, City = "Yinchuan", ProvinceCode = "NX", CurrencyCode = "CNY" },
                new Destination { DestinationID = 39, City = "Yumen", ProvinceCode = "GS", CurrencyCode = "CNY" },
                new Destination { DestinationID = 40, City = "Zhengzhou", ProvinceCode = "HA", CurrencyCode = "CNY" }
                );

            context.ShipmentTracking.AddOrUpdate(
                p => p.ShipmentTrackingID,
                new ShipmentTracking { ShipmentTrackingID = 1, WaybillId = 1, Arrived = true, DeliveredTo = "Monica Mok", DeliveredAt = "Front Door"}
                /*
                new ShipmentTracking { ShipmentTrackingID = 2, WaybillId = 2, Arrived = true, DeliveredTo = "George Guo", DeliveredAt = "Front Door" },
                new ShipmentTracking { ShipmentTrackingID = 3, WaybillId = 3, Arrived = true, DeliveredTo = "Sammy So", DeliveredAt = "Front Door" },
                new ShipmentTracking { ShipmentTrackingID = 4, WaybillId = 4, Arrived = false, DeliveredTo = "", DeliveredAt = "" }
                */
            );
            /*
            context.ShipmentStates.AddOrUpdate(
                p => p.ShipmentStateID,
                //for shipment 1
                new ShipmentState { ShipmentStateID = 1, ShipmentTrackingID = 1, Time = new DateTime(2017, 4, 6, 13, 55, 0), Description = "Picked Up", Location = "Hong Kong", Remarks = "Vehicle 34" },
                new ShipmentState { ShipmentStateID = 2, ShipmentTrackingID = 1, Time = new DateTime(2017, 4, 6, 16, 15, 0), Description = "At local sort facility", Location = "Tung Chung", Remarks = "" },
                new ShipmentState { ShipmentStateID = 3, ShipmentTrackingID = 1, Time = new DateTime(2017, 4, 6, 18, 05, 0), Description = "Left Origin", Location = "HKIA", Remarks = "CX0123" },
                new ShipmentState { ShipmentStateID = 4, ShipmentTrackingID = 1, Time = new DateTime(2017, 4, 6, 20, 18, 0), Description = "At local sort facility", Location = "Pudong", Remarks = "" },
                new ShipmentState { ShipmentStateID = 5, ShipmentTrackingID = 1, Time = new DateTime(2017, 4, 7, 20, 18, 0), Description = "On vehicle for delivery", Location = "Pudong", Remarks = "Vehicle 1032" },
                new ShipmentState { ShipmentStateID = 6, ShipmentTrackingID = 1, Time = new DateTime(2017, 4, 7, 20, 18, 0), Description = "Delivered", Location = "Shanghai", Remarks = "" }

                //for shipment 2
                new ShipmentState { ShipmentStateID = 7, ShipmentTrackingID = 2, Time = new DateTime(2017, 4, 10, 16, 45, 0), Description = "Picked Up", Location = "Hong Kong", Remarks = "Vehicle 12" },
                new ShipmentState { ShipmentStateID = 8, ShipmentTrackingID = 2, Time = new DateTime(2017, 4, 10, 20, 10, 0), Description = "At local sort facility", Location = "Tung Chung", Remarks = "" },
                new ShipmentState { ShipmentStateID = 9, ShipmentTrackingID = 2, Time = new DateTime(2017, 4, 11, 10, 18, 0), Description = "Left Origin", Location = "HKIA", Remarks = "KA3845" },
                new ShipmentState { ShipmentStateID = 10, ShipmentTrackingID = 2, Time = new DateTime(2017, 4, 11, 15, 28, 0), Description = "At local sort facility", Location = "Lanzhou", Remarks = "" },
                new ShipmentState { ShipmentStateID = 11, ShipmentTrackingID = 2, Time = new DateTime(2017, 4, 12, 07, 38, 0), Description = "On vehicle for delivery", Location = "Lanzhou", Remarks = "Vehicle 82" },
                new ShipmentState { ShipmentStateID = 12, ShipmentTrackingID = 2, Time = new DateTime(2017, 4, 12, 10, 13, 0), Description = "Delivered", Location = "Lanzhou", Remarks = "" },
                //for shipment 3
                new ShipmentState { ShipmentStateID = 13, ShipmentTrackingID = 3, Time = new DateTime(2017, 4, 14, 07, 55, 0), Description = "Picked Up", Location = "Hong Kong", Remarks = "Vehicle 13" },
                new ShipmentState { ShipmentStateID = 14, ShipmentTrackingID = 3, Time = new DateTime(2017, 4, 14, 09, 08, 0), Description = "At local sort facility", Location = "Tung Chung", Remarks = "" },
                new ShipmentState { ShipmentStateID = 15, ShipmentTrackingID = 3, Time = new DateTime(2017, 4, 14, 10, 18, 0), Description = "Left Origin", Location = "HKIA", Remarks = "KA3845" },
                new ShipmentState { ShipmentStateID = 16, ShipmentTrackingID = 3, Time = new DateTime(2017, 4, 14, 15, 28, 0), Description = "At local sort facility", Location = "Fuzhou", Remarks = "" },
                new ShipmentState { ShipmentStateID = 17, ShipmentTrackingID = 3, Time = new DateTime(2017, 4, 14, 15, 50, 0), Description = "On vehicle for delivery", Location = "Fuzhou", Remarks = "Vehicle 82" },
                new ShipmentState { ShipmentStateID = 18, ShipmentTrackingID = 3, Time = new DateTime(2017, 4, 14, 16, 53, 0), Description = "Delivered", Location = "Fuzhou", Remarks = "" },
                //for shipment 4
                new ShipmentState { ShipmentStateID = 19, ShipmentTrackingID = 4, Time = new DateTime(2017, 5, 2, 08, 30, 0), Description = "Picked Up", Location = "Hong Kong", Remarks = "Vehicle 12" },
                new ShipmentState { ShipmentStateID = 20, ShipmentTrackingID = 4, Time = new DateTime(2017, 5, 2, 10, 00, 0), Description = "At local sort facility", Location = "Shatin", Remarks = "" },
                new ShipmentState { ShipmentStateID = 21, ShipmentTrackingID = 4, Time = new DateTime(2017, 5, 2, 10, 35, 0), Description = "Left origin", Location = "Shatin", Remarks = "Vehicle 667" }
                );
            */

            /*
            // Add shipping accounts
            context.ShippingAccounts.AddOrUpdate(
                p => p.ShippingAccountId,
                new PersonalShippingAccount { ShippingAccountId = 1, UserName = "Personal", AccountType = "Personal", PhoneNumber = "61234567", Email = "comp3111_team119@cse.ust.hk", Building = "NULL", Street = "Test Street", City = "Hong Kong", Province = "HK", PostalCode = "NULL", CreditCardType = "American Express", CreditCardNumber = "1234567812345678", CreditCardSecurityNumber = "987", CardholderName = "Test Personal", CreditCardExpiryMonth = 1, CreditCardExpiryYear = 18, FirstName = "TestPersonal", LastName = "TestPersonal"},
                new BusinessShippingAccount { ShippingAccountId = 2, UserName = "Business", AccountType = "Business", PhoneNumber = "61234567", Email = "comp3111_team119@cse.ust.hk", Building = "NULL", Street = "Test Street", City = "Hong Kong", Province = "HK", PostalCode = "NULL", CreditCardType = "American Express", CreditCardNumber = "9876543219876543", CreditCardSecurityNumber = "123", CardholderName = "Test Business", CreditCardExpiryMonth = 12, CreditCardExpiryYear = 18, ContactPersonName = "Test Contact", CompanyName = "Test Company", DepartmentName = "NULL"}
            );
            */

        }
    }
}
