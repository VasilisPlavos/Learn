import { Body, Controller, Delete, Get, HttpException, HttpStatus, Param, Patch, Post, Req } from '@nestjs/common';
import { UserDto } from "./dtos/user.dto";
import { UsersService } from "./users.service";
import { ValidationPipe } from "src/utils/pipes/validation.pipe";

@Controller('users')
export class UsersController {
    constructor(private readonly usersService: UsersService) { }

    @Post()
    async create(
        @Body(new ValidationPipe()) userDto: UserDto, 
        @Req() request: Request,
        // @Res({ passthrough: true }) response: Response,
        // @Next({ passthrough: true }) next: NextFunction
) {
        console.log(request);
        const res = await this.usersService.save(userDto);
        return res;
    }

    @Get()
    findAll() {
        // throw new HttpException('Forbidden', HttpStatus.FORBIDDEN);
        return this.usersService.findAll();
    }

    @Get(':id')
    findOne(@Param('id') id: string) {
        return this.usersService.findOneById(+id);
    }

    @Patch()
    update(@Body() userDto: UserDto) {
        return this.usersService.update(userDto);
    }

    @Delete(':id')
    remove(@Param('id') id: string) {
        return this.usersService.delete(+id);
    }
}