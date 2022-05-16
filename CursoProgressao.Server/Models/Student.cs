using CursoProgressao.Server.Data;
using CursoProgressao.Server.Dto.Contacts;
using CursoProgressao.Server.Dto.Documents;
using CursoProgressao.Server.Dto.Residences;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Utils;
using System.Text;

namespace CursoProgressao.Server.Models;

public class Student : Model
{
    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            UpdateModificationDate();
        }
    }
    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            UpdateModificationDate();
        }
    }

    public string? Note
    {
        get => _note;
        set
        {
            _note = value;
            UpdateModificationDate();
        }
    }
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            UpdateModificationDate();
        }
    }
    public StudentDocument? Document
    {
        get => _document;
        private set
        {
            _document = value;
            UpdateModificationDate();
        }
    }
    public Contact? Contact
    {
        get => _contact;
        private set
        {
            _contact = value;
            UpdateModificationDate();
        }
    }
    public Residence? Residence
    {
        get => _residence;
        private set
        {
            _residence = value;
            UpdateModificationDate();
        }
    }
    public Guid? ResponsibleId
    {
        get => _responsibleId;
        set
        {
            _responsibleId = value;
            UpdateModificationDate();
        }
    }
    public Responsible? Responsible { get; private init; }

    private string _firstName;
    private string _lastName;
    private bool _isActive = true;
    private string? _note;
    private Guid? _responsibleId;
    private Contact? _contact;
    private Residence? _residence;
    private StudentDocument? _document;

    public Student(string firstName, string lastName, string? note = "", Guid? responsibleId = null) : base()
    {
        _firstName = firstName;
        _lastName = lastName;
        _responsibleId = responsibleId;
        _note = note;
    }

    public async Task SetDocumentAsync(SchoolContext context, UpdateDocumentDto dto, Func<Guid, Task<bool>> checkIdUniquenessAsync)
    {
        if (Document is null)
        {
            if (string.IsNullOrEmpty(dto.Rg) || string.IsNullOrEmpty(dto.Cpf)) throw new BadRequestException("InvalidStudentDocument", "Student document must have a valid rg and cpf");

            StudentDocument document = new(Id, dto.Rg, dto.Cpf);

            while (!await checkIdUniquenessAsync(document.Id)) document = new(Id, dto.Rg, dto.Cpf);

            context.StudentDocuments.Add(document);
            Document = document;

            return;
        }

        if (dto.Rg is not null) Document.Rg = dto.Rg;
        if (dto.Cpf is not null) Document.Cpf = dto.Cpf;

        UpdateModificationDate();
    }

    public async Task SetContactAsync(SchoolContext context, UpdateContactDto dto, Func<Guid, Task<bool>> checkIdUniquenessAsync)
    {
        if (Contact is null)
        {
            Contact contact = new(Id, dto.Email, dto.Landline, dto.CellPhone);

            while (!await checkIdUniquenessAsync(contact.Id)) contact = new(Id, dto.Email, dto.Landline, dto.CellPhone);

            context.Contacts.Add(contact);
            Contact = contact;

            return;
        }

        if (dto.CellPhone is not null) Contact.CellPhone = dto.CellPhone;
        if (dto.Landline is not null) Contact.Landline = dto.Landline;
        if (dto.Email is not null) Contact.Email = dto.Email;

        UpdateModificationDate();
    }

    public async Task SetResidenceAsync(SchoolContext context, UpdateResidenceDto dto, Func<Guid, Task<bool>> checkIdUniquenessAsync)
    {
        if (Residence is null)
        {
            Residence residence = new(Id, dto.ZipCode, dto.Address);

            while (!await checkIdUniquenessAsync(residence.Id)) residence = new(Id, dto.ZipCode, dto.Address);

            context.Residences.Add(residence);
            Residence = residence;

            return;
        }

        UpdateModificationDate();
    }

    public void RemoveProps(SchoolContext context, IEnumerable<string> props)
    {
        if (props.Count() > 25) throw new BadRequestException("RemoveListTooBig");

        List<string> remainingProps = new();

        foreach (string propToRemove in props)
        {
            string prop = propToRemove.ToPascalCase();

            switch (prop)
            {
                case "Note":
                    Note = null;
                    continue;
                case "Document":
                    if (Document is null) continue;
                    if (ResponsibleId is null) throw new BadRequestException("StudentWithoutInfo", "Cannot remove the document from a student without providing a responsible first");

                    context.StudentDocuments.Remove(Document);
                    UpdateModificationDate();
                    continue;
                case "ResponsibleId":
                    if (ResponsibleId is null) continue;
                    if (Document is null) throw new BadRequestException("StudentWithoutInfo", "Cannot remove the responsible from a student without providing a document first");

                    ResponsibleId = null;
                    UpdateModificationDate();
                    continue;
                case "Contact":
                    if (Contact is null) continue;

                    context.Contacts.Remove(Contact);
                    UpdateModificationDate();
                    continue;
                case "Residence":
                    if (Residence is null) continue;

                    context.Residences.Remove(Residence);
                    UpdateModificationDate();
                    continue;
                default:
                    remainingProps.Add(prop);
                    continue;
            }
        }

        if (Contact is not null) remainingProps = Contact.RemoveProps(remainingProps);
        if (Residence is not null) remainingProps = Residence.RemoveProps(remainingProps);

        if (remainingProps.Count > 0)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.Append(remainingProps[0]);

            for (int i = 1; i < remainingProps.Count; i++)
            {
                if (i == remainingProps.Count - 1) stringBuilder.Append(" and ");
                else stringBuilder.Append(", ");

                stringBuilder.Append($"{remainingProps[i]}");
            }

            throw new BadRequestException("InvalidProp", $"{stringBuilder} doesn't exist on model or it cannot be set to null");
        }
    }
}
