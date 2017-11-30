using Dharma.Common.Extensions;
using Dharma.Data.MongoDB.Entities;
using Dharma.Data.MongoDB.Exceptions;
using Dharma.Data.MongoDB.Providers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dharma.Data.MongoDB.Repositories
{
    /// <summary>
    /// Base para todos os outros repositórios.
    /// </summary>
    /// <typeparam name="T">Entidade do repositório.</typeparam>
    public class MongoRepository<T> where T : MongoEntityBase
    {
        /// <summary>
        /// Coleção para acesso aos dados do repositório.
        /// </summary>
        protected readonly IMongoCollection<T> _collection;
        private readonly MongoDatabaseProvider _mongoDatabaseProvider;

        /// <summary>
        /// Construtor do repositório.
        /// </summary>
        /// <param name="mongoDatabaseProvider">Fornecedor da conexão com o banco de dados.</param>
        public MongoRepository(MongoDatabaseProvider mongoDatabaseProvider)
        {
            _mongoDatabaseProvider = mongoDatabaseProvider;
            _collection = _mongoDatabaseProvider.Create().GetCollection<T>(TableName());
        }

        /// <summary>
        /// Insere um registro no banco de dados.
        /// </summary>
        /// <param name="entity">Dado que será inserido no banco de dados.</param>
        /// <returns>O objeto com o identificador único criado na base de dados.</returns>
        public T Create(T entity)
        {
            if (entity == null)
            {
                throw new EntityNullException("entity");
            }
            else if (entity.Id != null)
            {
                throw new EntityIdNullArgumentException("Id");
            }

            entity.Created = DateTime.UtcNow;
            entity.Updated = DateTime.UtcNow;

            _collection.InsertOne(entity);

            return entity;
        }

        /// <summary>
        /// Insere um registro no banco de dados.
        /// </summary>
        /// <param name="entity">Dado que será inserido no banco de dados.</param>
        /// <returns>O objeto com o identificador único criado na base de dados.</returns>
        public async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new EntityNullException("entity");
            }
            else if (entity.Id != null)
            {
                throw new EntityIdNullArgumentException("Id");
            }

            entity.Created = DateTime.UtcNow;
            entity.Updated = DateTime.UtcNow;

            await _collection.InsertOneAsync(entity);

            return entity;
        }

        /// <summary>
        /// Insere um lote de registros no banco de dados.
        /// </summary>
        /// <param name="entities">Dados que serão inseridos no banco de dados.</param>
        public void Create(List<T> entities)
        {
            if (entities == null)
            {
                throw new EntityNullException("entities");
            }

            bool existNullValue = false;
            bool existValidId = false;

            Parallel.ForEach(entities, (x) =>
            {
                if (existNullValue || existValidId)
                {
                    return;
                }
                else if (x == null)
                {
                    existNullValue = true;
                    return;
                }
                else if (x.Id != null)
                {
                    existValidId = true;
                    return;
                }

                x.Created = DateTime.UtcNow;
                x.Updated = DateTime.UtcNow;
            });

            if (existNullValue)
            {
                throw new EntityNullException("entities");
            }
            else if (existValidId)
            {
                throw new EntityIdNullArgumentException("Id");
            }

            _collection.InsertMany(entities);
        }

        /// <summary>
        /// Insere um lote de registros no banco de dados.
        /// </summary>
        /// <param name="entities">Dados que serão inseridos no banco de dados.</param>
        public async Task CreateAsync(List<T> entities)
        {
            if (entities == null)
            {
                throw new EntityNullException("entities");
            }

            bool existNullValue = false;
            bool existValidId = false;

            Parallel.ForEach(entities, (x) =>
            {
                if (existNullValue || existValidId)
                {
                    return;
                }
                else if (x == null)
                {
                    existNullValue = true;
                    return;
                }
                else if (x.Id != null)
                {
                    existValidId = true;
                    return;
                }

                x.Created = DateTime.UtcNow;
                x.Updated = DateTime.UtcNow;
            });

            if (existNullValue)
            {
                throw new EntityNullException("entities");
            }
            else if (existValidId)
            {
                throw new EntityIdNullArgumentException("Id");
            }

            await _collection.InsertManyAsync(entities);
        }

        /// <summary>
        /// Remove um registro do banco de dados a partir do identificador único (Id) da entidade informada.
        /// </summary>
        /// <param name="entity">
        /// Dado que será removido, utilizará o identificador único (Id) da entidade para remover o registro.
        /// </param>
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new EntityNullException("entity");
            }
            else if (!entity.Id.IsHash())
            {
                throw new EntityIdInvalidArgumentException("Id");
            }

            _collection.DeleteOne(p => p.Id.Equals(entity.Id));
        }

        /// <summary>
        /// Remove um registro do banco de dados a partir do identificador único (Id) da entidade informada.
        /// </summary>
        /// <param name="entity">
        /// Dado que será removido, utilizará o identificador único (Id) da entidade para remover o registro.
        /// </param>
        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new EntityNullException("entity");
            }
            else if (!entity.Id.IsHash())
            {
                throw new EntityIdInvalidArgumentException("Id");
            }

            await _collection.DeleteOneAsync(p => p.Id.Equals(entity.Id));
        }

        /// <summary>
        /// Remove um registro do banco de dados a partir do identificador único (Id).
        /// </summary>
        /// <param name="id">Identificador único do registro no banco de dados.</param>
        public void Delete(string id)
        {
            if (!id.IsHash())
            {
                throw new EntityIdInvalidArgumentException("id");
            }

            _collection.DeleteOne(p => p.Id.Equals(id));
        }

        /// <summary>
        /// Remove um registro do banco de dados a partir do identificador único (Id).
        /// </summary>
        /// <param name="id">Identificador único do registro no banco de dados.</param>
        public async Task DeleteAsync(string id)
        {
            if (!id.IsHash())
            {
                throw new EntityIdInvalidArgumentException("Id");
            }

            await _collection.DeleteOneAsync(p => p.Id.Equals(id));
        }

        /// <summary>
        /// Busca e retornar os dados de um registro a partir de um identificador único (Id).
        /// </summary>
        /// <param name="id">Identificador único do registro no banco de dados.</param>
        /// <returns>Dados do registro encontrado.</returns>
        public T Read(string id)
        {
            if (!id.IsHash())
            {
                throw new EntityIdInvalidArgumentException("id");
            }

            return _collection.Find(p => p.Id.Equals(id)).FirstOrDefault();
        }

        /// <summary>
        /// Busca e retornar os dados de um registro a partir de um identificador único (Id).
        /// </summary>
        /// <param name="id">Identificador único do registro no banco de dados.</param>
        /// <returns>Dados do registro encontrado.</returns>
        public async Task<T> ReadAsync(string id)
        {
            if (!id.IsHash())
            {
                throw new EntityIdInvalidArgumentException("id");
            }

            return await (await _collection.FindAsync(p => p.Id.Equals(id))).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Lista de registros paginada.
        /// </summary>
        /// <param name="page">Página solicitada.</param>
        /// <param name="pageSize">Número de registros por página.</param>
        /// <returns>Lista de registros encontrados na página.</returns>
        public List<T> Read(int page, int pageSize)
        {
            if (page <= 0)
            {
                throw new PaginationPageException("page");
            }
            else if (pageSize <= 0 || pageSize > 1000)
            {
                throw new PaginationPageSizeException("pageSize");
            }

            var result = _collection.Find(_ => true)
                .Skip(pageSize * (page - 1))
                .Limit(pageSize)
                .ToList();

            return result;
        }

        /// <summary>
        /// Lista de registros paginada.
        /// </summary>
        /// <param name="page">Página solicitada.</param>
        /// <param name="pageSize">Número de registros por página.</param>
        /// <returns>Lista de registros encontrados na página.</returns>
        public async Task<List<T>> ReadAsync(int page, int pageSize)
        {
            if (page <= 0)
            {
                throw new PaginationPageException("page");
            }
            else if (pageSize <= 0 || pageSize > 1000)
            {
                throw new PaginationPageSizeException("pageSize");
            }

            var result = await _collection.Find(_ => true)
                .Skip(pageSize * (page - 1))
                .Limit(pageSize)
                .ToListAsync();

            return result;
        }

        /// <summary>
        /// Lista de registros paginada.
        /// </summary>
        /// <param name="page">Página solicitada.</param>
        /// <param name="pageSize">Número de registros por página.</param>
        /// <param name="predicate">Filtro da consulta paginada.</param>
        /// <returns>Lista de registros encontrados na página.</returns>
        public List<T> Read(int page, int pageSize, Expression<Func<T, bool>> predicate)
        {
            if (page <= 0)
            {
                throw new PaginationPageException("page");
            }
            else if (pageSize <= 0 || pageSize > 1000)
            {
                throw new PaginationPageSizeException("pageSize");
            }
            else if (predicate == null)
            {
                throw new QueryException();
            }

            var result = _collection.Find(predicate)
                .Skip(pageSize * (page - 1))
                .Limit(pageSize)
                .ToList();

            return result;
        }

        /// <summary>
        /// Lista de registros paginada.
        /// </summary>
        /// <param name="page">Página solicitada.</param>
        /// <param name="pageSize">Número de registros por página.</param>
        /// <param name="predicate">Filtro da consulta paginada.</param>
        /// <returns>Lista de registros encontrados na página.</returns>
        public async Task<List<T>> ReadAsync(int page, int pageSize, Expression<Func<T, bool>> predicate)
        {
            if (page <= 0)
            {
                throw new PaginationPageException("page");
            }
            else if (pageSize <= 0 || pageSize > 1000)
            {
                throw new PaginationPageSizeException("pageSize");
            }
            else if (predicate == null)
            {
                throw new QueryException();
            }

            var result = await _collection.Find(predicate)
                .Skip(pageSize * (page - 1))
                .Limit(pageSize)
                .ToListAsync();

            return result;
        }

        /// <summary>
        /// Lista de registros encontrada a partir de um filtro.
        /// </summary>
        /// <param name="predicate">Filtro da consulta.</param>
        /// <returns>Lista de registros encontrados.</returns>
        public List<T> Read(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new QueryException();
            }

            var result = _collection.Find(predicate).ToList();

            return result;
        }

        /// <summary>
        /// Lista de registros encontrada a partir de um filtro.
        /// </summary>
        /// <param name="filter">Filtro da consulta.</param>
        /// <returns>Lista de registros encontrados.</returns>
        public List<T> Read(FilterDefinition<T> filter)
        {
            if (filter == null)
            {
                throw new QueryException();
            }

            var result = _collection.Find(filter).ToList();

            return result;
        }

        /// <summary>
        /// Lista de registros encontrada a partir de um filtro.
        /// </summary>
        /// <param name="predicate">Filtro da consulta.</param>
        /// <returns>Lista de registros encontrados.</returns>
        public async Task<List<T>> ReadAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new QueryException();
            }

            var result = await _collection.FindAsync(predicate);

            return await result.ToListAsync();
        }

        /// <summary>
        /// Lista de registros encontrada a partir de um filtro.
        /// </summary>
        /// <param name="filter">Filtro da consulta.</param>
        /// <returns>Lista de registros encontrados.</returns>
        public async Task<List<T>> ReadAsync(FilterDefinition<T> filter)
        {
            if (filter == null)
            {
                throw new QueryException();
            }

            var result = await _collection.FindAsync(filter);

            return await result.ToListAsync();
        }

        /// <summary>
        /// Obtém o primeiro registro encontrado a partir de um filtro.
        /// </summary>
        /// <param name="predicate">Filtro da consulta.</param>
        /// <returns>Obtém o primeiro de registro encontrado.</returns>
        public T ReadFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new QueryException();
            }

            var result = _collection.Find(predicate).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Obtém o primeiro registro encontrado a partir de um filtro.
        /// </summary>
        /// <param name="filter">Filtro da consulta.</param>
        /// <returns>Obtém o primeiro de registro encontrado.</returns>
        public T ReadFirstOrDefault(FilterDefinition<T> filter)
        {
            if (filter == null)
            {
                throw new QueryException();
            }

            var result = _collection.Find(filter).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Obtém o primeiro registro encontrado a partir de um filtro.
        /// </summary>
        /// <param name="predicate">Filtro da consulta.</param>
        /// <returns>Obtém o primeiro registro encontrados.</returns>
        public async Task<T> ReadFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new QueryException();
            }

            var result = await _collection.Find(predicate).FirstOrDefaultAsync();

            return result;
        }

        /// <summary>
        /// Obtém o primeiro registro encontrado a partir de um filtro.
        /// </summary>
        /// <param name="filter">Filtro da consulta.</param>
        /// <returns>Obtém o primeiro de registro encontrado.</returns>
        public async Task<T> ReadFirstOrDefaultAsync(FilterDefinition<T> filter)
        {
            if (filter == null)
            {
                throw new QueryException();
            }

            var result = await _collection.FindAsync(filter);

            return await result.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Número de registros existentes.
        /// </summary>
        /// <returns>Número de registros existentes.</returns>
        public long Count()
        {
            return _collection.Count(_ => true);
        }

        /// <summary>
        /// Número de registros existentes.
        /// </summary>
        /// <returns>Número de registros existentes.</returns>
        public async Task<long> CountAsync()
        {
            return await _collection.CountAsync(_ => true);
        }

        /// <summary>
        /// Número de registros existentes para um filtro.
        /// </summary>
        /// <returns>Número de registros existentes para um filtro.</returns>
        public long Count(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new QueryException();
            }

            return _collection.Count(predicate);
        }

        /// <summary>
        /// Número de registros existentes para um filtro.
        /// </summary>
        /// <returns>Número de registros existentes para um filtro.</returns>
        public async Task<long> CountAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new QueryException();
            }

            return await _collection.CountAsync(predicate);
        }

        /// <summary>
        /// Atualiza os dados de um registro, caso ele não exista será criado.
        /// </summary>
        /// <param name="entity">Dados do registro a ser atualizado.</param>
        /// <returns>Dados do registro atualizado.</returns>
        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new EntityNullException("entity");
            }
            else if (!entity.Id.IsHash())
            {
                throw new EntityIdInvalidArgumentException("Id");
            }

            var document = Read(entity.Id);

            if (document == null)
            {
                throw new EntityNotExistException(entity.Id);
            }

            entity.Updated = DateTime.UtcNow;

            _collection.ReplaceOne(_ => _.Id == entity.Id, entity);

            return entity;
        }

        /// <summary>
        /// Atualiza os dados de um registro, caso ele não exista será criado.
        /// </summary>
        /// <param name="entity">Dados do registro a ser atualizado.</param>
        /// <returns>Dados do registro atualizado.</returns>
        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new EntityNullException("entity");
            }
            else if (!entity.Id.IsHash())
            {
                throw new EntityIdInvalidArgumentException("Id");
            }

            var document = await ReadAsync(entity.Id);

            if (document == null)
            {
                throw new EntityNotExistException(entity.Id);
            }

            entity.Updated = DateTime.UtcNow;

            await _collection.ReplaceOneAsync(_ => _.Id == entity.Id, entity);

            return entity;
        }

        /// <summary>
        /// Pode ser implementado na herança para criação de índices de forma automática.
        /// </summary>
        public virtual void CreateIndexes()
        {

        }

        /// <summary>
        /// Nome da tabela obtido a partir do nome do objeto da entidade genérico.
        /// </summary>
        /// <returns>Nome da tabela.</returns>
        public virtual string TableName()
        {
            string tableName = typeof(T).Name;

            if (tableName.EndsWith("Entity"))
            {
                tableName = tableName.Substring(0, tableName.IndexOf("Entity"));
            }

            return tableName;
        }
    }
}
