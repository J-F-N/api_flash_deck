using Microsoft.AspNetCore.Identity;
using MongoDB.EntityFrameworkCore;

namespace api_flash_deck.Models;

[Collection("Users")]
public class AppUserIdentity : IdentityUser<Guid>
{ }