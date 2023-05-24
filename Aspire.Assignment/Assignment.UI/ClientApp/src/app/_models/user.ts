export class User {
    id?: number;
    username?: string;
  password?: string;
  emailaddress?: string;
  lastname?: string;
  firstname?: string;
    token?: string;
}

export class TokenResponse {
    userName?: string;
    token?: string;
}
