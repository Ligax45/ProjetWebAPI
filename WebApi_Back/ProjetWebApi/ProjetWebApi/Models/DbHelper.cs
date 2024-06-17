﻿using ProjetWebApi.Data.DatabaseContext;
using ProjetWebApi.Data.Entities;

namespace ProjetWebApi.Models
{
    public class DbHelper
    {
        private DataContext _context;
        public DbHelper(DataContext context)
        {
            _context = context;
        }

        public List<UtilisateurModel> GetAllUtilisateurs()
        {
            List<UtilisateurModel> response = new List<UtilisateurModel>();
            var dataList = _context.Utilisateurs.ToList();
            dataList.ForEach(row => response.Add(new UtilisateurModel()
            {
                Id = row.Id,
                Email = row.Email,
                Password = row.Password,
                Salt = row.Salt,
            }));
            return response;
        }

        public UtilisateurModel GetUtilisateurById(int id)
        {
            var row = _context.Utilisateurs.FirstOrDefault(d => d.Id == id);
            if (row != null)
            {
                return new UtilisateurModel()
                {
                    Id = row.Id,
                    Email = row.Email,
                    Password = row.Password,
                    Salt = row.Salt,
                };
            }
            return null;
        }

        public void CreateUtilisateur(UtilisateurModel utilisateurModel, string salt)
        {
            Utilisateur dbTable = new Utilisateur
            {
                Email = utilisateurModel.Email,
                Password = utilisateurModel.Password,
                Salt = salt
            };

            _context.Utilisateurs.Add(dbTable);
            _context.SaveChanges();
        }

        public void UpdateUtilisateur(UtilisateurModel utilisateurModel, string salt)
        {
            Utilisateur dbTable = _context.Utilisateurs.FirstOrDefault(d => d.Id == utilisateurModel.Id);

            if (dbTable != null)
            {
                dbTable.Email = utilisateurModel.Email;
                dbTable.Password = utilisateurModel.Password;
                dbTable.Salt = salt;

                _context.SaveChanges();
            }
        }

        public void DeleteUtilisateur(int id)
        {
            var utilisateur = _context.Utilisateurs.FirstOrDefault(d => d.Id == id);
            if (utilisateur != null)
            {
                _context.Utilisateurs.Remove(utilisateur);
                _context.SaveChanges();
            }
        }
    }
}
