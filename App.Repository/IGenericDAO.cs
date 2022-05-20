using System;
using System.Collections.Generic;

namespace SGED.Repository
{
    public interface IGenericDAO<T>
    {
        Boolean salvar(T obj);
        List<T> listar(String busca);
        T carregar(int idObject);
        Boolean excluir(int idObject);
    }
}
