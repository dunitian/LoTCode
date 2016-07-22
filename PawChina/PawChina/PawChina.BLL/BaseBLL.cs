using PawChina.IBLL;
using PawChina.IDal;

namespace PawChina.BLL
{
    public class BaseBLL: IBaseBLL
    {
        protected IBaseDal baseDal = Factory.DalFactory.Resolve<IBaseDal>();
    }
}
