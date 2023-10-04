using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Services.Base;
using Microsoft.EntityFrameworkCore;
using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.API.Controllers.Base
{
    [Authorize]
    public abstract class CrudController<TContext, TEntity, TCreateRequest, TUpdateRequest, TViewModel, TKey> : ApiController
        where TEntity : BaseEntity<TKey>, new()
        where TCreateRequest : BaseCreateRequest, new()
        where TUpdateRequest : BaseUpdateRequest<TKey>, new()
        where TViewModel : BaseViewModel<TKey>, new()
        where TContext : DbContext
    {
        private readonly IBaseService<TContext, TEntity, TCreateRequest, TUpdateRequest, TViewModel, TKey> _baseService;
        protected CrudController(ILogger logger, IBaseService<TContext, TEntity, TCreateRequest, TUpdateRequest, TViewModel, TKey> baseService) : base(logger)
        {
            _baseService = baseService;
        }
        [HttpPost]
        public virtual async Task<ActionResult> Post([FromForm] TCreateRequest? request)
        {
            if (null == request)
                return BadRequest();
            var result = await _baseService.CreateAsync(request);

            if (null == result)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Put(TKey id, [FromForm] TUpdateRequest request)
        {
            if (!id!.Equals(request.Id))
                return BadRequest();
            try
            {
                return Ok(await _baseService.UpdateAsync(id, request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(TKey id)
        {
            try
            {
                var result = await _baseService.DeleteSoftAsync(id);
                if (result > 0)
                    return Ok();
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            return NoContent();
        }
        [HttpPost("filter")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public abstract Task<ActionResult<IBasePaging<TViewModel>>> GetPaging(FilterBodyRequest request);
        [HttpGet("{id}")]
        public virtual async Task<ActionResult> GetById(TKey id)
        {
            var result = await _baseService.GetByIdAsync(id);

            if (result != null)
                return Ok(result);
            return StatusCode(500);
        }
    }
}
