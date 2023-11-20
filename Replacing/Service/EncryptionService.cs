using Replacing.DB;
using Replacing.Models;
using System;
using System.Text;

namespace Replacing.Service;

public class EncryptionService 
{
    private readonly Dictionary<string, string> replaceDictionary;
    private readonly DataContext _context;

    public EncryptionService(DataContext context)
    {
        _context = context;
        // Предварительная загрузка замен из базы данных
        var replaceWords = context.ReplaceWords.ToList();
        replaceDictionary = replaceWords.ToDictionary(r => r.OldSymbol, r => r.NewSymbol);
    }

    public string EncryptText(string originalText)
    {
        // Замена каждой буквы в тексте
        StringBuilder encryptedText = new StringBuilder();
        foreach (char character in originalText)
        {
            if (replaceDictionary.TryGetValue(character.ToString(), out var replacement))
            {
                encryptedText.Append(replacement);
            }
            else
            {
                encryptedText.Append(character);
            }
        }

        return encryptedText.ToString();
    }
}
