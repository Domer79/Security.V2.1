import { Observable } from "rxjs/Observable";

export interface RepositoryBase<TEntity> {
    getAll(): Observable<TEntity[]>;
    getElement(id: number): Observable<TEntity>;
    create(object: TEntity): Observable<TEntity>;
    update(object: TEntity): Promise<void>;
    remove(object: TEntity): Promise<void>;
}
