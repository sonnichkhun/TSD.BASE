using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mdbo_file
{
    public interface Idbo_fileValidator : IServiceScoped
    {
        Task<bool>
    Create(dbo_file dbo_file);
    Task<bool>
        Update(dbo_file dbo_file);
        Task<bool>
            Delete(dbo_file dbo_file);
            Task<bool>
                BulkDelete(List<dbo_file>
                    dbo_files);
                    Task<bool>
                        Import(List<dbo_file>
                            dbo_files);
                            }

                            public class dbo_fileValidator : Idbo_fileValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public dbo_fileValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(dbo_file dbo_file)
                                {
                                dbo_fileFilter dbo_fileFilter = new dbo_fileFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = dbo_file.Id },
                                Selects = dbo_fileSelect.Id
                                };

                                int count = await UOW.dbo_fileRepository.Count(dbo_fileFilter);
                                if (count == 0)
                                dbo_file.AddError(nameof(dbo_fileValidator), nameof(dbo_file.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(dbo_file dbo_file)
                                    {
                                    return dbo_file.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(dbo_file dbo_file)
                                        {
                                        if (await ValidateId(dbo_file))
                                        {
                                        }
                                        return dbo_file.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(dbo_file dbo_file)
                                            {
                                            if (await ValidateId(dbo_file))
                                            {
                                            }
                                            return dbo_file.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<dbo_file>
                                                    dbo_files)
                                                    {
                                                    foreach (dbo_file dbo_file in dbo_files)
                                                    {
                                                    await Delete(dbo_file);
                                                    }
                                                    return dbo_files.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<dbo_file>
                                                            dbo_files)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
