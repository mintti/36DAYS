using System;
namespace Days.Infra.Interface
{
    public interface IState {
        ushort Hp {get;set;}
        ushort Speed {get;set;}
        ushort Gage {get;set;}
    }
}