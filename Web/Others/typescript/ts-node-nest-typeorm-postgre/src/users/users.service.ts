import { Injectable } from "@nestjs/common";
import { UserDto } from "./dtos/user.dto";

@Injectable()
export class UsersService {
    create(user: UserDto){
        return user;
    }

    findAll() {
      return `This action returns all users`;
    }
  
    findOne(id: number) {
      return `This action returns a #${id} user`;
    }

    update(user: UserDto){
        return `This action updates a #${user.id} user`;
    }

    remove(id: number){
        return `This action removes a #${id} user`;
    }
}