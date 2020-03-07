using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MTOS.Models
{
    public class MTService
    {
        private readonly MTDBEntities _Entity;

        public MTService()
        {
            _Entity = new MTDBEntities();
        }

        #region Common

        async protected Task<string> Save<T>(EntityState action, T item)
            where T : class, new()
        {
            bool success = false;
            _Entity.Entry<T>(item).State = action;
            int saved = await _Entity.SaveChangesAsync();
            if (action == EntityState.Added || action == EntityState.Deleted)
            {
                success = saved > 0;
            }
            else if (action == EntityState.Modified)
            {
                success = saved >= 0;
            }

            string action_name = action.ToString();
            if (action == EntityState.Added)
                action_name = "新增";
            else if (action == EntityState.Modified)
                action_name = "修改";
            else if (action == EntityState.Deleted)
                action_name = "刪除";
            return action_name + (success ? "成功" : "失敗");
        }

        #endregion

        #region COMPANY_BASIC_INFO

        public IQueryable<COMPANY_BASIC_INFO> LookupCOMPANY_BASIC_INFO()
        {
            return _Entity.COMPANY_BASIC_INFO.AsNoTracking();
        }

        public COMPANY_BASIC_INFO GetCOMPANY_BASIC(string xInfoType)
        {
            return LookupCOMPANY_BASIC_INFO().SingleOrDefault(s => s.INFO_TYPE == xInfoType);
        }

        async public Task<string> AddCOMPANY_BASIC(COMPANY_BASIC_INFO item)
        {
            return await Save<COMPANY_BASIC_INFO>(EntityState.Added, item);
        }

        async public Task<string> UpdateCOMPANY_BASIC(COMPANY_BASIC_INFO item)
        {
            return await Save<COMPANY_BASIC_INFO>(EntityState.Modified, item);
        }

        async public Task<string> DeleteCOMPANY_BASIC(COMPANY_BASIC_INFO item)
        {
            return await Save<COMPANY_BASIC_INFO>(EntityState.Deleted, item);
        }

        #endregion

        #region CONTACT_US

        public IQueryable<CONTACT_US> LookupCONTACT_US()
        {
            return _Entity.CONTACT_US.AsNoTracking();
        }

        public CONTACT_US GetCONTACT_US(string xGUID)
        {
            return LookupCONTACT_US().SingleOrDefault(s => s.GUID == xGUID);
        }

        async public Task<string> AddCONTACT_US(CONTACT_US item)
        {
            item.GUID = Guid.NewGuid().ToString();
            return await Save<CONTACT_US>(EntityState.Added, item);
        }

        async public Task<string> UpdateCONTACT_US(CONTACT_US item)
        {
            return await Save<CONTACT_US>(EntityState.Modified, item);
        }

        async public Task<string> DeleteCONTACT_US(CONTACT_US item)
        {
            return await Save<CONTACT_US>(EntityState.Deleted, item);
        }

        #endregion

        #region SERIES

        public IQueryable<SERIES> LookupSERIES()
        {
            return _Entity.SERIES.AsNoTracking().AsQueryable();
        }

        public SERIES GetSERIES(string xID)
        {
            return LookupSERIES().SingleOrDefault(s => s.ID == xID);
        }

        async public Task<string> AddSERIES(SERIES item)
        {
            return await Save<SERIES>(EntityState.Added, item);
        }

        async public Task<string> UpdateSERIES(SERIES item)
        {
            return await Save<SERIES>(EntityState.Modified, item);
        }

        async public Task<string> DeleteSERIES(SERIES item)
        {
            return await Save<SERIES>(EntityState.Deleted, item);
        }

        #endregion

        #region PRODUCT

        public IQueryable<PRODUCT> LookupPRODUCT(string xSERIES = null)
        {
            var q = _Entity.PRODUCT.AsNoTracking().AsQueryable();
            if (xSERIES.IsNotNullOrEmpty())
                q = q.Where(w => w.SERIES == xSERIES);

            return q;
        }

        public PRODUCT GetPRODUCT(string xSERIES, string xID)
        {
            return LookupPRODUCT(xSERIES).SingleOrDefault(s => s.ID == xID);
        }

        async public Task<string> AddPRODUCT(PRODUCT item)
        {
            return await Save<PRODUCT>(EntityState.Added, item);
        }

        async public Task<string> UpdatePRODUCT(PRODUCT item)
        {
            return await Save<PRODUCT>(EntityState.Modified, item);
        }

        async public Task<string> DeletePRODUCT(PRODUCT item)
        {
            return await Save<PRODUCT>(EntityState.Deleted, item);
        }

        #endregion

        #region PRODUCT_DOCUMENT

        public IQueryable<PRODUCT_DOCUMENT> LookupPRODUCT_DOCUMENT(ReportQueryViewModel filter)
        {
            var q = _Entity.PRODUCT_DOCUMENT.AsNoTracking().AsQueryable();
            if (filter.IsNotNull())
            {
                if (filter.TradeDate_S.HasValue)
                    q = q.Where(w => w.REPORT_DATE >= filter.TradeDate_S.Value);
                if (filter.TradeDate_E.HasValue)
                    q = q.Where(w => w.REPORT_DATE <= filter.TradeDate_E.Value);
                if (filter.Series.IsNotNullOrEmpty())
                    q = q.Where(w => w.SERIES == filter.Series);
                if (filter.ProductID.IsNotNullOrEmpty())
                    q = q.Where(w => w.PRODUCT_ID == filter.ProductID);
                if (filter.DocumentType.IsNotNullOrEmpty())
                    q = q.Where(w => w.DOCUMENT_TYPE == filter.DocumentType);
            }

            return q;
        }

        public PRODUCT_DOCUMENT GetPRODUCT_DOCUMENT(string xGUID)
        {
            return LookupPRODUCT_DOCUMENT(null).SingleOrDefault(s => s.GUID == xGUID);
        }

        async public Task<string> AddPRODUCT_DOCUMENT(PRODUCT_DOCUMENT item)
        {
            return await Save<PRODUCT_DOCUMENT>(EntityState.Added, item);
        }

        async public Task<string> UpdatePRODUCT_DOCUMENT(PRODUCT_DOCUMENT item)
        {
            item.MODIFY_DATE = DateTime.Now;
            return await Save<PRODUCT_DOCUMENT>(EntityState.Modified, item);
        }

        async public Task<string> DeletePRODUCT_DOCUMENT(PRODUCT_DOCUMENT item)
        {
            return await Save<PRODUCT_DOCUMENT>(EntityState.Deleted, item);
        }

        #endregion
    }
}