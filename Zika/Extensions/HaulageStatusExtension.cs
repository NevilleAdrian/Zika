using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zika.Extensions
{
    public static class HaulageStatusExtension
    {
        public static string StatusReadable(this Enum.HaulageStatus haulage)
        {
            switch (haulage)
            {
                case Enum.HaulageStatus.AwaitingFreightAndClearingPayment:
                    return "Awaiting freight and clearing payment";
                case Enum.HaulageStatus.OrderArrivedAndCleared:
                    return "Order arrived and cleared";
                case Enum.HaulageStatus.OrderArrivedAtWarehouse:
                    return "Order arrived at warehouse";
                case Enum.HaulageStatus.OrderReadyForPickup:
                    return "Order ready for pickup";
                case Enum.HaulageStatus.PaymentConfirmed:
                    return "Payment confirmed";
                case Enum.HaulageStatus.ShipmentRecieved:
                    return "Shipment recieved";
                default:
                    return null;
            }
        }
    }
}
