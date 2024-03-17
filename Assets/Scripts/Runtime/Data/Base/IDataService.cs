/// <summary>
/// ���ݷ���ӿ� �ṩ���ݷ���������
/// </summary>
public interface IDataService
{
    /// <summary>
    /// ��������
    /// </summary>
    /// <typeparam name="T">�������������</typeparam>
    /// <param name="relativePath">���·��</param>
    /// <param name="data">���ݶ���</param>
    /// <param name="encrypted">�Ƿ����</param>
    public bool SaveData<T>(string relativePath,T data, bool encrypted);

    /// <summary>
    /// �������� jsonתΪ����
    /// </summary>
    /// <typeparam name="T">��������</typeparam>
    /// <param name="relativePath">���·��</param>
    /// <param name="encrypted">�Ƿ����</param>
    /// <returns></returns>
    public T LoadData<T>(string relativePath,bool encrypted);
}
