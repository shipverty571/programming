﻿using NoteApp.DAL.Interfaces;
using NoteApp.Domain.Entity;

namespace NoteApp.DAL;

public class NoteRepository : INoteRepository
{
    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    private readonly AppDbContext _appDbContext;

    /// <summary>
    /// Создает экземпляр класса <see cref="NoteRepository"/>.
    /// </summary>
    /// <param name="appDbContext">Контекст базы данных.</param>
    public NoteRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Create(NoteEntity note)
    {
        await _appDbContext.Notes.AddAsync(note);
        await _appDbContext.SaveChangesAsync();
    }

    public IQueryable<NoteEntity> GetAll()
    {
        return _appDbContext.Notes;
    }

    public async Task Update(NoteEntity note)
    {
        _appDbContext.Notes.Update(note);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Delete(NoteEntity note)
    {
        _appDbContext.Notes.Remove(note);
        await _appDbContext.SaveChangesAsync();
    }
}