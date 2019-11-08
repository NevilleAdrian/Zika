using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zika.Data;
using Zika.Models;
using Zika.Services;

namespace Zika.Repository
{
    public class InvoiceRepository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ILogger<InvoiceRepository> _log;
        private readonly IEmailSender _email;
        public InvoiceRepository(ApplicationDbContext context,
            ILogger<InvoiceRepository> logger,
            IEmailSender email)
        {
            _ctx = context;
            _log = logger;
            _email = email;

        }

        public async Task<bool> Add(Invoice invoice)
        {
            try
            {
                _ctx.Invoices.Add(invoice);
                await _ctx.SaveChangesAsync();

                
            }
            catch (Exception ex)
            {
                _log.LogInformation($"Cannot create Invoice: {ex.Message}");
                return false;
            }
            try
            {
                Freight freight = await _ctx.Freights.Where(x => x.FreightId == invoice.FreightId).Include(x => x.User).FirstOrDefaultAsync();
                ApplicationUser user = freight.User;
                string message = $"Dear {user.LastName},";
                message += $"<br/><br/>An invoice with freight number {invoice.FreightId} has been generated for you. Please kindly make your payment.";
                message += "<br/><br/>Thank you.";
                message += "<br/><br/>";
                string css = @"
                                .invoice-title h2, .invoice-title h3 {
                                    display: inline - block;
                                            }

                                .table > tbody > tr > .no - line {
                                                border - top: none;
                                            }

                                .table > thead > tr > .no - line {
                                                border - bottom: none;
                                            }

                                .table > tbody > tr > .thick - line {
                                                border - top: 2px solid;
                                            }";
                message += $@"<html>
                                
                                    <style>{css}</style>


                        <div class='row'>
                                <div class='col-xs-12'>

                                    <div class='invoice-title'>
                                        <h2>Invoice</h2><h3 class='pull-right'>Freight # {invoice.FreightId}</h3>
                                    </div>
                                    <hr>
                                    <div class='row'>
                                        <div class='col-xs-6'>
                                            <address>
                                                <strong>Billed To:</strong><br>
                                                {user.LastName}, {user.FirstName}<br>
                                                {freight.FreightTo}
                                            </address>
                                        </div>
                                        <div class='col-xs-6 text-right'>
                                            <address>
                                                <strong>Shipped To:</strong><br>
                                                {user.LastName}, {user.FirstName}<br>
                                                {freight.FreightTo}
                                            </address>
                                        </div>
                                    </div>
                                    <div class='row'>
                                        <div class='col-xs-6'>
                                        </div>
                                        <div class='col-xs-6 text-right'>
                                            <address>
                                                <strong>Order Date:</strong><br>
                                                {DateTime.Now.ToShortDateString()}<br><br>
                                            </address>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class='row'>
                                <div class='col-md-12'>
                                    <div class='panel panel-default'>
                                        <div class='panel-heading'>
                                            <h3 class='panel-title'><strong>Order summary</strong></h3>
                                        </div>
                                        <div class='panel-body'>
                                            <div class='table-responsive'>
                                                <table class='table table-condensed no-border'>
                                                    <thead>
                                                        <tr>
                                                            <td><strong>Description</strong></td>
                                                            <td><strong>Mass</strong></td>
                                                            <td><strong>Amount</strong></td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>

                                                        <tr>
                                                            <td>{freight.FreightSummary}</td>
                                                            <td>{freight.Mass}</td>
                                                            <td>{freight.Amount}</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class='z-mt-30' rowspan='2' colspan='2'>
                                                                {invoice.Extra ?? ""}
                                                            </td>
                                                            <td><strong>Total: </strong>{freight.Amount}</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                                                             <script src='https://ajax.aspnetcdn.com/ajax/jquery/jquery-2.2.0.min.js'></script>
                                                            <script src = 'https://ajax.aspnetcdn.com/ajax/bootstrap/3.3.7/bootstrap.min.js'></script>
                                                        
                                                        ";
                await _email.SendEmailAsync(user.Email, "INVOICE", message);
            }
            catch { }
            return true;
        }

        public async Task<Invoice> Item(int id)
        {
            if (Exists(id))
            {
                return await _ctx.Invoices.Where(x => x.InvoiceId == id).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<Invoice>> Items() => await _ctx.Invoices.ToListAsync() ?? new List<Invoice>();

        public async Task<bool> Update(Invoice invoice)
        {
            if (Exists(invoice.InvoiceId))
            {
                try
                {
                    _ctx.Invoices.Update(invoice);
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"Cannot update Invoice: {ex.Message}");

                }
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            if (Exists(id))
            {
                var interest = await _ctx.Invoices.FindAsync(id);
                try
                {
                    _ctx.Invoices.Remove(interest);
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"Cannot delete Invoice: {ex.Message}");

                }
            }
            return false;
        }

        private bool Exists(int id) => _ctx.Invoices.Any(x => x.InvoiceId == id);
    }
}
