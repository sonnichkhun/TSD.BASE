using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mdbo_notification
{
    public interface Idbo_notificationValidator : IServiceScoped
    {
        Task<bool>
    Create(dbo_notification dbo_notification);
    Task<bool>
        Update(dbo_notification dbo_notification);
        Task<bool>
            Delete(dbo_notification dbo_notification);
            Task<bool>
                BulkDelete(List<dbo_notification>
                    dbo_notifications);
                    Task<bool>
                        Import(List<dbo_notification>
                            dbo_notifications);
                            }

                            public class dbo_notificationValidator : Idbo_notificationValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public dbo_notificationValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(dbo_notification dbo_notification)
                                {
                                dbo_notificationFilter dbo_notificationFilter = new dbo_notificationFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = dbo_notification.Id },
                                Selects = dbo_notificationSelect.Id
                                };

                                int count = await UOW.dbo_notificationRepository.Count(dbo_notificationFilter);
                                if (count == 0)
                                dbo_notification.AddError(nameof(dbo_notificationValidator), nameof(dbo_notification.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(dbo_notification dbo_notification)
                                    {
                                    return dbo_notification.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(dbo_notification dbo_notification)
                                        {
                                        if (await ValidateId(dbo_notification))
                                        {
                                        }
                                        return dbo_notification.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(dbo_notification dbo_notification)
                                            {
                                            if (await ValidateId(dbo_notification))
                                            {
                                            }
                                            return dbo_notification.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<dbo_notification>
                                                    dbo_notifications)
                                                    {
                                                    foreach (dbo_notification dbo_notification in dbo_notifications)
                                                    {
                                                    await Delete(dbo_notification);
                                                    }
                                                    return dbo_notifications.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<dbo_notification>
                                                            dbo_notifications)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
