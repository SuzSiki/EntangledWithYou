using System;



//フィールド上にあるものの
//player側が見えているもの
public interface IFieldSurface
{
    ObjectAttribute attribute{get;}
}

public enum ObjectAttribute
{
    none = 0,
    isSolid = 1 << 0,       //重なれない
    isContactable = 1 << 1,  //何か起こる
    isAttractable = 1 << 2

}