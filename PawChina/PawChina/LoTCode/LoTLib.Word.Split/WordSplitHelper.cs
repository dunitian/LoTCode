using System.Linq;
using JiebaNet.Segmenter;
using System.Collections.Generic;
using JiebaNet.Analyser;

namespace LoTLib.Word.Split
{
    #region 分词类型
    public enum JiebaTypeEnum
    {
        /// <summary>
        /// 精确模式---最基础和自然的模式，试图将句子最精确地切开，适合文本分析
        /// </summary>
        Default,
        /// <summary>
        /// 全模式---可以成词的词语都扫描出来, 速度更快，但是不能解决歧义
        /// </summary>
        CutAll,
        /// <summary>
        /// 搜索引擎模式---在精确模式的基础上对长词再次切分，提高召回率，适合用于搜索引擎分词
        /// </summary>
        CutForSearch,
        /// <summary>
        /// 精确模式-不带HMM
        /// </summary>
        Other
    }
    #endregion

    /// <summary>
    /// 结巴分词
    /// </summary>
    public static partial class WordSplitHelper
    {
        #region 公用系列
        /// <summary>
        /// 获取分词之后的字符串集合
        /// </summary>
        /// <param name="objStr"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetSplitWords(string objStr, JiebaTypeEnum type = JiebaTypeEnum.Default)
        {
            var jieba = new JiebaSegmenter();
            switch (type)
            {
                case JiebaTypeEnum.Default:
                    return jieba.Cut(objStr);                 //精确模式-带HMM
                case JiebaTypeEnum.CutAll:
                    return jieba.Cut(objStr, cutAll: true);   //全模式
                case JiebaTypeEnum.CutForSearch:
                    return jieba.CutForSearch(objStr);        //搜索引擎模式
                default:
                    return jieba.Cut(objStr, false, false);   //精确模式-不带HMM
            }
        }

        /// <summary>
        /// 提取文章关键词集合
        /// </summary>
        /// <param name="objStr"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetArticleKeywords(string objStr)
        {
            var idf = new TfidfExtractor();
            return idf.ExtractTags(objStr, 10, Constants.NounAndVerbPos);//名词和动词
        }

        /// <summary>
        /// 返回拼接后的字符串
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static string JoinKeyWords(IEnumerable<string> words)
        {
            //没结果则返回空字符串
            if (words == null || words.Count() < 1)
            {
                return string.Empty;
            }
            words = words.Distinct();//有时候词有重复的，得自己处理一下
            return string.Join(",", words);//根据个人需求返回
        }
        #endregion

        #region 扩展相关
        /// <summary>
        /// 获取分词之后的字符串
        /// </summary>
        /// <param name="objStr"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSplitWordStr(this string objStr, JiebaTypeEnum type = JiebaTypeEnum.Default)
        {
            var words = GetSplitWords(objStr, type);
            return JoinKeyWords(words);
        }

        /// <summary>
        /// 提取文章关键词字符串
        /// </summary>
        /// <param name="objStr"></param>
        /// <returns></returns>
        public static string GetArticleKeywordStr(this string objStr)
        {
            var words = GetArticleKeywords(objStr);
            return JoinKeyWords(words);
        }
        #endregion
    }
}