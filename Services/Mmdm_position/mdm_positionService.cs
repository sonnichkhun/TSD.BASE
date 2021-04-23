using BASE.Common;
using BASE.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using BASE.Repositories;
using BASE.Entities;

namespace BASE.Services.Mmdm_position
{
    public interface Imdm_positionService :  IServiceScoped
    {
        Task<int>
    Count(mdm_positionFilter mdm_positionFilter);
    Task<List<mdm_position>> List(mdm_positionFilter mdm_positionFilter);
        Task<mdm_position> Get(long Id);
        Task<mdm_position> Create(mdm_position mdm_position);
        Task<mdm_position> Update(mdm_position mdm_position);
        Task<mdm_position> Delete(mdm_position mdm_position);
        Task<List<mdm_position>> BulkDelete(List<mdm_position> mdm_positions);
        Task<List<mdm_position>> Import(List<mdm_position> mdm_positions);
        mdm_positionFilter ToFilter(mdm_positionFilter mdm_positionFilter);
    }

    public class mdm_positionService : BaseService, Imdm_positionService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Imdm_positionValidator mdm_positionValidator;

        public mdm_positionService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Imdm_positionValidator mdm_positionValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.mdm_positionValidator = mdm_positionValidator;
        }
        public async Task<int> Count(mdm_positionFilter mdm_positionFilter)
        {
            try
            {
                int result = await UOW.mdm_positionRepository.Count(mdm_positionFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_positionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_positionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_position>> List(mdm_positionFilter mdm_positionFilter)
        {
            try
            {
                List<mdm_position> mdm_positions = await UOW.mdm_positionRepository.List(mdm_positionFilter);
                return mdm_positions;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_positionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_positionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<mdm_position> Get(long Id)
        {
            mdm_position mdm_position = await UOW.mdm_positionRepository.Get(Id);
            if (mdm_position == null)
                return null;
            return mdm_position;
        }

        public async Task<mdm_position> Create(mdm_position mdm_position)
        {
            if (!await mdm_positionValidator.Create(mdm_position))
                return mdm_position;

            try
            {
                await UOW.Begin();
                await UOW.mdm_positionRepository.Create(mdm_position);
                await UOW.Commit();
                mdm_position = await UOW.mdm_positionRepository.Get(mdm_position.Id);
                await Logging.CreateAuditLog(mdm_position, new { }, nameof(mdm_positionService));
                return mdm_position;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_positionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_positionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_position> Update(mdm_position mdm_position)
        {
            if (!await mdm_positionValidator.Update(mdm_position))
                return mdm_position;
            try
            {
                var oldData = await UOW.mdm_positionRepository.Get(mdm_position.Id);

                await UOW.Begin();
                await UOW.mdm_positionRepository.Update(mdm_position);
                await UOW.Commit();

                mdm_position = await UOW.mdm_positionRepository.Get(mdm_position.Id);
                await Logging.CreateAuditLog(mdm_position, oldData, nameof(mdm_positionService));
                return mdm_position;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_positionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_positionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_position> Delete(mdm_position mdm_position)
        {
            if (!await mdm_positionValidator.Delete(mdm_position))
                return mdm_position;

            try
            {
                await UOW.Begin();
                await UOW.mdm_positionRepository.Delete(mdm_position);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_position, nameof(mdm_positionService));
                return mdm_position;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_positionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_positionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_position>> BulkDelete(List<mdm_position> mdm_positions)
        {
            if (!await mdm_positionValidator.BulkDelete(mdm_positions))
                return mdm_positions;

            try
            {
                await UOW.Begin();
                await UOW.mdm_positionRepository.BulkDelete(mdm_positions);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_positions, nameof(mdm_positionService));
                return mdm_positions;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_positionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_positionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_position>> Import(List<mdm_position> mdm_positions)
        {
            if (!await mdm_positionValidator.Import(mdm_positions))
                return mdm_positions;
            try
            {
                await UOW.Begin();
                await UOW.mdm_positionRepository.BulkMerge(mdm_positions);
                await UOW.Commit();

                await Logging.CreateAuditLog(mdm_positions, new { }, nameof(mdm_positionService));
                return mdm_positions;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_positionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_positionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public mdm_positionFilter ToFilter(mdm_positionFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<mdm_positionFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                mdm_positionFilter subFilter = new mdm_positionFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
