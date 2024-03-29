﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject
{
    /// <summary>
    /// Holds the business logic for searching invoices
    /// </summary>
    class clsSearchLogic
    {
        #region class fields
        /// <summary>
        /// Data access object for making queries on the database
        /// </summary>
        clsDataAccess dataAccess = new clsDataAccess();

        /// <summary>
        /// Holds all of the SQL statements
        /// </summary>
        clsSearchSQL sql = new clsSearchSQL();

        /// <summary>
        /// This is the interface for interacting with the invoice window
        /// This interface will be used to pass the selected Invoice's number back to the invoice window
        /// </summary>
        InvoiceInterface invoiceInterface;
        #endregion

        #region constructor
        public clsSearchLogic()
        {
            try
            {
                this.dataAccess = new clsDataAccess();

                this.sql = new clsSearchSQL();
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Sets the instance of the InvoiceInterface for the class
        /// </summary>
        /// <param name="invoiceInterface"></param>
        public void SetView(InvoiceInterface invoiceInterface)
        {
            try
            {
                this.invoiceInterface = invoiceInterface;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Passes the invoice number to the InvoiceWindow 
        /// It will be set as the active invoice
        /// The InvoiceWindow will be refreshed with the active invoice info
        /// </summary>
        /// <param name="invoice"></param>
        public void InvoiceSelected(clsMainLogic invoice)
        {
            invoiceInterface.SetInvoice(invoice.InvoiceNum);
        }

        /// <summary>
        /// Returns a list of all of the invoices in the database
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<clsMainLogic> GetInvoices()
        {
            ObservableCollection<clsMainLogic> result = new ObservableCollection<clsMainLogic>();

            try
            {
                int iRet = 0;
                DataSet ds = dataAccess.ExecuteSQLStatement(sql.SelectAllInvoices(), ref iRet);
                for (int i = 0; i < iRet; ++i)
                {
                    clsMainLogic invoice = new clsMainLogic();
                    invoice.InvoiceNum = Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString());
                    invoice.InvoiceDate = Convert.ToDateTime(ds.Tables[0].Rows[i][1].ToString());
                    invoice.TotalCharge = Convert.ToDecimal(ds.Tables[0].Rows[i][2].ToString());
                    result.Add(invoice);
                }
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Returns a list of all of the distinct invoice numbers in the database
        /// </summary>
        /// <returns>List of invoice numbers</returns>
        public List<String> GetInvoiceNums()
        {
            List<String> result = new List<String>();

            try
            {
                int iRet = 0;
                DataSet ds = dataAccess.ExecuteSQLStatement(sql.SelectAllInvoiceNumbers(), ref iRet);
                for (int i = 0; i < iRet; ++i)
                {
                    result.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Returns a list of all of the distinct invoice dates in the database
        /// </summary>
        /// <returns>List of invoice dates</returns>
        public List<String> GetInvoiceDates()
        {
            List<String> result = new List<String>();

            try
            {
                int iRet = 0;
                DataSet ds = dataAccess.ExecuteSQLStatement(sql.SelectAllInvoiceDate(), ref iRet);
                for (int i = 0; i < iRet; ++i)
                {
                    result.Add(Convert.ToDateTime(ds.Tables[0].Rows[i][0].ToString()).ToString("d"));
                }
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Returns a list of all of the distinct total costs for an invoice in the database
        /// </summary>
        /// <returns>List total costs</returns>
        public List<String> GetTotalCost()
        {
            List<String> result = new List<String>();

            try
            {
                int iRet = 0;
                DataSet ds = dataAccess.ExecuteSQLStatement(sql.SelectAllInvoiceTotalCost(), ref iRet);
                for (int i = 0; i < iRet; ++i)
                {
                    result.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

            return result;
        }
        #endregion
    }
}
