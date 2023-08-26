using Microsoft.AspNetCore.DataProtection;

namespace SchoolWebsite.shared;
public class DataProtector
{
    private IDataProtector _dataProtector;

    public DataProtector(IDataProtectionProvider protectionProvider) => _dataProtector = protectionProvider.CreateProtector(GetType().FullName);
    public Student Encrypt(Student student)
    {
        return new Student
        {
            Id = student.Id,
            Name = _dataProtector.Protect(student.Name),
            Age = _dataProtector.Protect(student.Age),
            Phone = _dataProtector.Protect(student.Phone)
        };
    }
    public List<Student> Encrypt(List<Student> students) =>
        students.Select(student => Encrypt(student)).ToList();

    public Student Decrypt(Student student)
    {
        return new Student
        {
            Id = student.Id,
            Name = _dataProtector.Unprotect(student.Name),
            Age = _dataProtector.Unprotect(student.Age),
            Phone = _dataProtector.Unprotect(student.Phone)
        };
    }
    public IEnumerable<Student> Decrypt(List<Student> students) =>
        students.Select(student => Decrypt(student));
}

