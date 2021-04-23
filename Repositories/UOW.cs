using BASE.Common;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using BASE.Models;
using BASE.Repositories;

namespace BASE.Repositories
{
    public interface IUOW : IServiceScoped
    {
        Task Begin();
        Task Commit();
        Task Rollback();

        Idbo_fileRepository dbo_fileRepository { get; }
        Idbo_mailtemplateRepository dbo_mailtemplateRepository { get; }
        Idbo_notificationRepository dbo_notificationRepository { get; }
        Ienum_currencyRepository enum_currencyRepository { get; }
        Ienum_emailstatusRepository enum_emailstatusRepository { get; }
        Ienum_emailtypeRepository enum_emailtypeRepository { get; }
        Ienum_filetypeRepository enum_filetypeRepository { get; }
        Ienum_notificationstatusRepository enum_notificationstatusRepository { get; }
        Ienum_paymentstatusRepository enum_paymentstatusRepository { get; }
        Ienum_sexRepository enum_sexRepository { get; }
        Ienum_statusRepository enum_statusRepository { get; }
        Imdm_appuserRepository mdm_appuserRepository { get; }
        Imdm_districtRepository mdm_districtRepository { get; }
        Imdm_imageRepository mdm_imageRepository { get; }
        Imdm_nationRepository mdm_nationRepository { get; }
        Imdm_organizationRepository mdm_organizationRepository { get; }
        Imdm_phonetypeRepository mdm_phonetypeRepository { get; }
        Imdm_positionRepository mdm_positionRepository { get; }
        Imdm_professionRepository mdm_professionRepository { get; }
        Imdm_provinceRepository mdm_provinceRepository { get; }
        Imdm_taxtypeRepository mdm_taxtypeRepository { get; }
        Imdm_unitofmeasureRepository mdm_unitofmeasureRepository { get; }
        Imdm_unitofmeasuregroupingRepository mdm_unitofmeasuregroupingRepository { get; }
        Imdm_unitofmeasuregroupingcontentRepository mdm_unitofmeasuregroupingcontentRepository { get; }
        Imdm_wardRepository mdm_wardRepository { get; }
        Iper_actionRepository per_actionRepository { get; }
        Iper_fieldRepository per_fieldRepository { get; }
        Iper_fieldtypeRepository per_fieldtypeRepository { get; }
        Iper_menuRepository per_menuRepository { get; }
        Iper_pageRepository per_pageRepository { get; }
        Iper_permissionRepository per_permissionRepository { get; }
        Iper_permissioncontentRepository per_permissioncontentRepository { get; }
        Iper_permissionoperatorRepository per_permissionoperatorRepository { get; }
        Iper_roleRepository per_roleRepository { get; }
    }

    public class UOW : IUOW
    {
        private DataContext DataContext;

        public Idbo_fileRepository dbo_fileRepository { get; private set; }
        public Idbo_mailtemplateRepository dbo_mailtemplateRepository { get; private set; }
        public Idbo_notificationRepository dbo_notificationRepository { get; private set; }
        public Ienum_currencyRepository enum_currencyRepository { get; private set; }
        public Ienum_emailstatusRepository enum_emailstatusRepository { get; private set; }
        public Ienum_emailtypeRepository enum_emailtypeRepository { get; private set; }
        public Ienum_filetypeRepository enum_filetypeRepository { get; private set; }
        public Ienum_notificationstatusRepository enum_notificationstatusRepository { get; private set; }
        public Ienum_paymentstatusRepository enum_paymentstatusRepository { get; private set; }
        public Ienum_sexRepository enum_sexRepository { get; private set; }
        public Ienum_statusRepository enum_statusRepository { get; private set; }
        public Imdm_appuserRepository mdm_appuserRepository { get; private set; }
        public Imdm_districtRepository mdm_districtRepository { get; private set; }
        public Imdm_imageRepository mdm_imageRepository { get; private set; }
        public Imdm_nationRepository mdm_nationRepository { get; private set; }
        public Imdm_organizationRepository mdm_organizationRepository { get; private set; }
        public Imdm_phonetypeRepository mdm_phonetypeRepository { get; private set; }
        public Imdm_positionRepository mdm_positionRepository { get; private set; }
        public Imdm_professionRepository mdm_professionRepository { get; private set; }
        public Imdm_provinceRepository mdm_provinceRepository { get; private set; }
        public Imdm_taxtypeRepository mdm_taxtypeRepository { get; private set; }
        public Imdm_unitofmeasureRepository mdm_unitofmeasureRepository { get; private set; }
        public Imdm_unitofmeasuregroupingRepository mdm_unitofmeasuregroupingRepository { get; private set; }
        public Imdm_unitofmeasuregroupingcontentRepository mdm_unitofmeasuregroupingcontentRepository { get; private set; }
        public Imdm_wardRepository mdm_wardRepository { get; private set; }
        public Iper_actionRepository per_actionRepository { get; private set; }
        public Iper_fieldRepository per_fieldRepository { get; private set; }
        public Iper_fieldtypeRepository per_fieldtypeRepository { get; private set; }
        public Iper_menuRepository per_menuRepository { get; private set; }
        public Iper_pageRepository per_pageRepository { get; private set; }
        public Iper_permissionRepository per_permissionRepository { get; private set; }
        public Iper_permissioncontentRepository per_permissioncontentRepository { get; private set; }
        public Iper_permissionoperatorRepository per_permissionoperatorRepository { get; private set; }
        public Iper_roleRepository per_roleRepository { get; private set; }

        public UOW(DataContext DataContext)
        {
            this.DataContext = DataContext;

dbo_fileRepository = new dbo_fileRepository(DataContext);
dbo_mailtemplateRepository = new dbo_mailtemplateRepository(DataContext);
dbo_notificationRepository = new dbo_notificationRepository(DataContext);
enum_currencyRepository = new enum_currencyRepository(DataContext);
enum_emailstatusRepository = new enum_emailstatusRepository(DataContext);
enum_emailtypeRepository = new enum_emailtypeRepository(DataContext);
enum_filetypeRepository = new enum_filetypeRepository(DataContext);
enum_notificationstatusRepository = new enum_notificationstatusRepository(DataContext);
enum_paymentstatusRepository = new enum_paymentstatusRepository(DataContext);
enum_sexRepository = new enum_sexRepository(DataContext);
enum_statusRepository = new enum_statusRepository(DataContext);
mdm_appuserRepository = new mdm_appuserRepository(DataContext);
mdm_districtRepository = new mdm_districtRepository(DataContext);
mdm_imageRepository = new mdm_imageRepository(DataContext);
mdm_nationRepository = new mdm_nationRepository(DataContext);
mdm_organizationRepository = new mdm_organizationRepository(DataContext);
mdm_phonetypeRepository = new mdm_phonetypeRepository(DataContext);
mdm_positionRepository = new mdm_positionRepository(DataContext);
mdm_professionRepository = new mdm_professionRepository(DataContext);
mdm_provinceRepository = new mdm_provinceRepository(DataContext);
mdm_taxtypeRepository = new mdm_taxtypeRepository(DataContext);
mdm_unitofmeasureRepository = new mdm_unitofmeasureRepository(DataContext);
mdm_unitofmeasuregroupingRepository = new mdm_unitofmeasuregroupingRepository(DataContext);
mdm_unitofmeasuregroupingcontentRepository = new mdm_unitofmeasuregroupingcontentRepository(DataContext);
mdm_wardRepository = new mdm_wardRepository(DataContext);
per_actionRepository = new per_actionRepository(DataContext);
per_fieldRepository = new per_fieldRepository(DataContext);
per_fieldtypeRepository = new per_fieldtypeRepository(DataContext);
per_menuRepository = new per_menuRepository(DataContext);
per_pageRepository = new per_pageRepository(DataContext);
per_permissionRepository = new per_permissionRepository(DataContext);
per_permissioncontentRepository = new per_permissioncontentRepository(DataContext);
per_permissionoperatorRepository = new per_permissionoperatorRepository(DataContext);
per_roleRepository = new per_roleRepository(DataContext);
        }
        public async Task Begin()
        {
            await DataContext.Database.BeginTransactionAsync();
        }

        public Task Commit()
        {
            DataContext.Database.CommitTransaction();
            return Task.CompletedTask;
        }

        public Task Rollback()
        {
            DataContext.Database.RollbackTransaction();
            return Task.CompletedTask;
        }
    }
}