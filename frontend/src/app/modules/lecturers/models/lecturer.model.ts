import { deserialize, serialize } from "cerialize";

export class Lecturer {

    @deserialize
    id: string;

    @serialize
    @deserialize
    name: string;

    @serialize
    @deserialize
    surname: string;

    @serialize
    @deserialize
    mail: string;

    @deserialize
    roleTypeID: number;

    @serialize
    @deserialize
    phone: string;

    @deserialize
    title: string;
}

export const Roles = [
    'Admin',
    'Zwykły użytkownik'
]