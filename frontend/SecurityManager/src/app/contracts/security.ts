import { UserRepository } from "./repositories/user-repository";

export interface Security {
    userValidate(): boolean;
    checkAccess(): boolean;
    setPassword(): boolean;

    UsersService: UserRepository;
}
