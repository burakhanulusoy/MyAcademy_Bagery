using Bagery.WebUI.Models.CartModels;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CartCommands;

// Fiyat burada YOK: fiyatı istemci göndermez, handler veritabanından okur.
// VariantId soru işaretli çünkü kullanıcı seçenek seçmeyebilir (null = standart ürün).
public record AddToCartCommand(Guid ProductId, Guid? VariantId, int Quantity) : IRequest<Cart>;