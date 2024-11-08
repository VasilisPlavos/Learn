import { PipeTransform, Injectable, ArgumentMetadata, BadRequestException } from '@nestjs/common';
import { validate } from 'class-validator';
import { plainToInstance } from 'class-transformer';

@Injectable()
export class ValidationPipe implements PipeTransform<any> {
  async transform(value: any) {
      return value;
  }

  // this is an example but needs to be fixed
  // async transform(value: any, { metatype }: ArgumentMetadata) {
  //   try {
  //       if (!metatype) {
  //           return value;
  //         }
  //         const object = plainToInstance(metatype, value);
  //         await validate(object);
  //         return value;
  //   } catch (error) {
  //       console.log(error);
  //       throw new BadRequestException('Validation failed');   
  //   }
  // }

}