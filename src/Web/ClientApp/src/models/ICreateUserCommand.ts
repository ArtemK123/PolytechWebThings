import { UserRole } from "./UserRole";

export interface ICreateUserCommand {
    email: string;
    password: string;
    role: UserRole;
}