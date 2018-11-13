import { RepositoryBase } from "./repository-base";
import { Observable } from "rxjs/Observable";

export interface SecurityRepositoryBase<TEntity> extends RepositoryBase<TEntity> {
    createEmpty(prefix: string): Observable<TEntity>;
    getByName(name: string): Observable<TEntity>;
}
