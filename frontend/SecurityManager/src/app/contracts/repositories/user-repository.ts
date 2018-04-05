import { User } from "../models/user";
import { SecurityRepositoryBase } from "./base/security-repository-base";

export interface UserRepository extends SecurityRepositoryBase<User> {
    SetStatus(loginOrEmail: string, status: boolean): Promise<void>;
}
