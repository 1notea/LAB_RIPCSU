using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zakharov; // Импортируйте пространство имен с моделью Journal
using Zakharov.Models;

public class JournalRepository
{
    private readonly AppDbContext _context;

    public JournalRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Journal> GetAllJournals()
    {
        return _context.Journals.ToList();
    }

    public Journal GetJournalById(int id)
    {
        return _context.Journals.FirstOrDefault(j => j.Id == id);
    }

    public IEnumerable<Journal> GetJournalsByStudentName(string studentFullName)
    {
        return _context.Journals.Where(j => j.StudentFullName == studentFullName).ToList();
    }

    public void CreateJournal(Journal journal)
    {
        _context.Journals.Add(journal);
        _context.SaveChanges();
    }

    public void UpdateJournal(Journal journal)
    {
        _context.Entry(journal).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void DeleteJournal(int id)
    {
        var journal = _context.Journals.Find(id);
        if (journal != null)
        {
            _context.Journals.Remove(journal);
            _context.SaveChanges();
        }
    }
}
