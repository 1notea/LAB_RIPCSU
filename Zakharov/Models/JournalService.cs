using System;
using System.Collections.Generic;
using Zakharov;
using Zakharov.Models;

public class JournalService
{
    private readonly JournalRepository _repository;

    public JournalService(JournalRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Journal> GetAllJournals()
    {
        return _repository.GetAllJournals();
    }

    public Journal GetJournalById(int id)
    {
        return _repository.GetJournalById(id);
    }

    public IEnumerable<Journal> GetJournalsByStudentName(string studentFullName)
    {
        return _repository.GetJournalsByStudentName(studentFullName);
    }

    public void CreateJournal(Journal journal)
    {
        _repository.CreateJournal(journal);
    }

    public void UpdateJournal(Journal journal)
    {
        _repository.UpdateJournal(journal);
    }

    public void DeleteJournal(int id)
    {
        _repository.DeleteJournal(id);
    }
}
