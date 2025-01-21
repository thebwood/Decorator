﻿using Decorator.BusinessLayer.Services.Interfaces;
using Decorator.ClassLibrary.Dtos;
using Decorator.ClassLibrary.DTOs;
using Decorator.ClassLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Decorator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressDomainService;
        public AddressesController(IAddressService addressDomainService)
        {
            _addressDomainService = addressDomainService;
        }

        [HttpGet]
        public async Task<ActionResult<GetAddressesResponseDTO>> GetAllAddresses()
        {
            GetAddressesResponseDTO response = new GetAddressesResponseDTO();
            List<AddressModel> addresses = await _addressDomainService.GetAllAddresses();
            List<AddressDTO> addressDTOs = addresses.Select(a => new AddressDTO(a.Id, a.StreetAddress, a.StreetAddress2, a.City, a.State, a.PostalCode)).ToList();

            response.AddressList = addressDTOs;

            return Ok(response);
        }
        [HttpPost]
        [Route("filter")]
        public async Task<ActionResult<GetAddressesResponseDTO>> GetAddressesByFilter([FromBody] GetAddressesRequestDTO requestDTO)
        {
            GetAddressesResponseDTO response = new GetAddressesResponseDTO();
            List<AddressModel> addresses = await _addressDomainService.GetAddressesByFilter(requestDTO);
            List<AddressDTO> addressDTOs = addresses.Select(a => new AddressDTO(a.Id, a.StreetAddress, a.StreetAddress2, a.City, a.State, a.PostalCode)).ToList();

            response.AddressList = addressDTOs;

            return Ok(response);

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetAddressResponseDTO>> GetAddressById(Guid id)
        {
            GetAddressResponseDTO response = new GetAddressResponseDTO();
            AddressModel? address = await _addressDomainService.GetAddressById(id);
            if (address == null)
            {
                return NotFound();
            }
            AddressDTO addressDTO = new AddressDTO(address.Id, address.StreetAddress, address.StreetAddress2, address.City, address.State, address.PostalCode);
            response.Address = addressDTO;
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResultModel>> CreateAddress([FromBody] AddAddressRequestDTO requestDTO)
        {
            ResultModel response = new();
            try
            {

                AddressModel address = new(null, requestDTO.Address.StreetAddress, requestDTO.Address.StreetAddress2, requestDTO.Address.City, requestDTO.Address.State, requestDTO.Address.PostalCode);
                AddressModel createdAddress = await _addressDomainService.CreateAddress(address);

                response.StatusCode = HttpStatusCode.Created;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResultModel>> UpdateAddress([FromBody] UpdateAddressRequestDTO requestDTO)
        {
            ResultModel<AddressDTO> response = new();


            try
            {
                AddressModel? address = await _addressDomainService.GetAddressById(requestDTO.Address.Id.Value);
                if (address == null)
                {

                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Success = false;
                    return NotFound(response);

                }


                address.StreetAddress = requestDTO.Address.StreetAddress;
                address.StreetAddress2 = requestDTO.Address.StreetAddress2;
                address.City = requestDTO.Address.City;
                address.State = requestDTO.Address.State;
                address.PostalCode = requestDTO.Address.PostalCode;

                AddressModel updatedAddress = await _addressDomainService.UpdateAddress(address);
                AddressDTO updatedAddressDTO = new AddressDTO(updatedAddress.Id, updatedAddress.StreetAddress, updatedAddress.StreetAddress2, updatedAddress.City, updatedAddress.State, updatedAddress.PostalCode);

                response.StatusCode = HttpStatusCode.OK;
                response.Success = true;
                response.Value = updatedAddressDTO;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAddress(Guid id)
        {
            AddressModel? address = await _addressDomainService.GetAddressById(id);
            if (address == null)
            {
                return NotFound();
            }
            await _addressDomainService.DeleteAddress(id);
            return Ok();
        }

    }
}
