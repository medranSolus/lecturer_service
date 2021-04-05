import { deserialize } from "cerialize";

export class Lecturer {

    @deserialize
    id: string;

    @deserialize
    name: string;

    @deserialize
    surname: string;

    @deserialize
    mail: string;

    @deserialize
    roleTypeID: number;

    @deserialize
    phone: string;

    @deserialize
    title: string;
}