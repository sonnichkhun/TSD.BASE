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

namespace BASE.Services.Mper_action
{
    public interface Iper_actionService :  IServiceScoped
    {
        Task<int>
    Count(per_actionFilter per_actionFilter);
    Task<List<per_action>> List(per_actionFilter per_actionFilter);
        Task<per_action> Get(long Id);
        Task<per_action> Create(per_action per_action);
        Task<per_action> Update(per_action per_action);
        Task<per_action> Delete(per_action per_action);
        Task<List<per_action>> BulkDelete(List<per_action> per_actions);
        Task<List<per_action>> Import(List<per_action> per_actions);
        per_actionFilter ToFilter(per_actionFilter per_actionFilter);
    }

    public class per_actionService : BaseService, Iper_actionService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Iper_actionValidator per_actionValidator;

        public per_actionService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Iper_actionValidator per_actionValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.per_actionValidator = per_actionValidator;
        }
        public async Task<int> Count(per_actionFilter per_actionFilter)
        {
            try
            {
                int result = await UOW.per_actionRepository.Count(per_actionFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_actionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_actionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_action>> List(per_actionFilter per_actionFilter)
        {
            try
            {
                List<per_action> per_actions = await UOW.per_actionRepository.List(per_actionFilter);
                return per_actions;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_actionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_actionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<per_action> Get(long Id)
        {
            per_action per_action = await UOW.per_actionRepository.Get(Id);
            if (per_action == null)
                return null;
            return per_action;
        }

        public async Task<per_action> Create(per_action per_action)
        {
            if (!await per_actionValidator.Create(per_action))
                return per_action;

            try
            {
                await UOW.Begin();
                await UOW.per_actionRepository.Create(per_action);
                await UOW.Commit();
                per_action = await UOW.per_actionRepository.Get(per_action.Id);
                await Logging.CreateAuditLog(per_action, new { }, nameof(per_actionService));
                return per_action;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_actionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_actionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_action> Update(per_action per_action)
        {
            if (!await per_actionValidator.Update(per_action))
                return per_action;
            try
            {
                var oldData = await UOW.per_actionRepository.Get(per_action.Id);

                await UOW.Begin();
                await UOW.per_actionRepository.Update(per_action);
                await UOW.Commit();

                per_action = await UOW.per_actionRepository.Get(per_action.Id);
                await Logging.CreateAuditLog(per_action, oldData, nameof(per_actionService));
                return per_action;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_actionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_actionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_action> Delete(per_action per_action)
        {
            if (!await per_actionValidator.Delete(per_action))
                return per_action;

            try
            {
                await UOW.Begin();
                await UOW.per_actionRepository.Delete(per_action);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_action, nameof(per_actionService));
                return per_action;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_actionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_actionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_action>> BulkDelete(List<per_action> per_actions)
        {
            if (!await per_actionValidator.BulkDelete(per_actions))
                return per_actions;

            try
            {
                await UOW.Begin();
                await UOW.per_actionRepository.BulkDelete(per_actions);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_actions, nameof(per_actionService));
                return per_actions;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_actionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_actionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_action>> Import(List<per_action> per_actions)
        {
            if (!await per_actionValidator.Import(per_actions))
                return per_actions;
            try
            {
                await UOW.Begin();
                await UOW.per_actionRepository.BulkMerge(per_actions);
                await UOW.Commit();

                await Logging.CreateAuditLog(per_actions, new { }, nameof(per_actionService));
                return per_actions;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_actionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_actionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public per_actionFilter ToFilter(per_actionFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<per_actionFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                per_actionFilter subFilter = new per_actionFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.MenuId))
                        subFilter.MenuId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
