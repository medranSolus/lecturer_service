import { deserialize, deserializeAs, serialize } from "cerialize";
import { Group } from "../../schedule/models/group.model";

export const Departments = [
    'Wydział Architektury',
    'Wydział Budownictwa Lądowego i Wodnego',
    'Wydział Chemiczny',
    'Wydział Elektroniki',
    'Wydział Elektryczny',
    'Wydział Geoinżynierii, Górnictwa i Geologii',
    'Wydział Inżynierii Środowiska',
    'Wydział Informatyki i Zarządzania',
    'Wydział Mechaniczno-Energetyczny',
    'Wydział Mechaniczny',
    'Wydział Podstawowych Problemów Techniki',
    'Wydział Elektroniki Mikrosystemów i Fotoniki',
    'Wydział Matematyki',
    'Filia w Jeleniej Górze',
    'Filia w Wałbrzychu',
    'Filia w Legnicy'
]

export class CourseListItem {
    departmentID: number;
    departmentName: string;
    courses: CourseShort[];
}

export class CourseShort {
    @deserialize
    id: string;

    @deserialize
    name: string;

    @deserialize
    departmentID: number;

    @deserialize
    lecturerID: string;

    @deserialize
    typeID: number;

    @deserialize
    courseGroup: string;
}

export const CourseType = [
    'Inne',
    'Wykład',
    'Laboratoria',
    'Ćwiczenia',
    'Projekt',
    'Seminarium'
]

export class Course {
    @serialize
    id: string;

    @serialize
    name: string;

    @serialize
    departmentID: number;

    @serialize
    lecturerID: string;

    @serialize
    typeID: number;

    @serialize
    courseGroup: string;
}

export const LanguageType = [
    'Polski',
    'Angielski',
    'Niemiecki'
]

export class GroupRequest {
    
    @deserialize
    id: string

    @deserializeAs(Group)
    group: Group
}

export class GroupsByID {
    id: string;
    groups: GroupRequest[];

    constructor(id, group) {
        this.id = id;
        this.groups = [group];
    }
}