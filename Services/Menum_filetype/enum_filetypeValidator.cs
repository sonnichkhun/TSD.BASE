using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Menum_filetype
{
    public interface Ienum_filetypeValidator : IServiceScoped
    {
        Task<bool>
    Create(enum_filetype enum_filetype);
    Task<bool>
        Update(enum_filetype enum_filetype);
        Task<bool>
            Delete(enum_filetype enum_filetype);
            Task<bool>
                BulkDelete(List<enum_filetype>
                    enum_filetypes);
                    Task<bool>
                        Import(List<enum_filetype>
                            enum_filetypes);
                            }

                            public class enum_filetypeValidator : Ienum_filetypeValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public enum_filetypeValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(enum_filetype enum_filetype)
                                {
                                enum_filetypeFilter enum_filetypeFilter = new enum_filetypeFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = enum_filetype.Id },
                                Selects = enum_filetypeSelect.Id
                                };

                                int count = await UOW.enum_filetypeRepository.Count(enum_filetypeFilter);
                                if (count == 0)
                                enum_filetype.AddError(nameof(enum_filetypeValidator), nameof(enum_filetype.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(enum_filetype enum_filetype)
                                    {
                                    return enum_filetype.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(enum_filetype enum_filetype)
                                        {
                                        if (await ValidateId(enum_filetype))
                                        {
                                        }
                                        return enum_filetype.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(enum_filetype enum_filetype)
                                            {
                                            if (await ValidateId(enum_filetype))
                                            {
                                            }
                                            return enum_filetype.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<enum_filetype>
                                                    enum_filetypes)
                                                    {
                                                    foreach (enum_filetype enum_filetype in enum_filetypes)
                                                    {
                                                    await Delete(enum_filetype);
                                                    }
                                                    return enum_filetypes.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<enum_filetype>
                                                            enum_filetypes)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
