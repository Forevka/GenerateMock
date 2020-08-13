export interface IGetMe {
    userId:       string;
    createdDate:  Date;
    userSecurity: UserSecurity;
}

export interface UserSecurity {
    login:  string;
    roleId: string;
    role:   Role;
}

export interface Role {
    roleId:   string;
    roleName: string;
}
