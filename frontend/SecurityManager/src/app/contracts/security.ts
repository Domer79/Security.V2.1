import { UserRepository } from "./repositories/user-repository";
import { Observable } from "rxjs";

export interface Security {
    checkAccess(policy: string): Observable<boolean>;
}
